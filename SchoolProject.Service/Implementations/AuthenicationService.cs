using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using static SchoolProject.Data.Results.JwtAuthResult;
using System.Security.Cryptography;
using SchoolProject.infrastructure.Abstract;
using Azure.Core;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SchoolProject.Data.Results;

namespace SchoolProject.Service.Implementations
{
    public class AuthenicationService : IAuthenicationService
    {


        #region fields
        private readonly UserManager<User> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly IRefershTokenRepository _refershTokenRepository;
        
        #endregion
        #region ctor
        public AuthenicationService(UserManager<User> userManager,JwtSettings jwtSettings,IRefershTokenRepository refershTokenRepository)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
            _refershTokenRepository = refershTokenRepository;
           
        }

        #endregion
        #region function
        public async Task< JwtAuthResult> GetJWTToken(User user)
        {
           
          var(JwtToken, accessToken) = await GenerateJWTToken(user);
             var refershToken = GetRefreshToken(user.UserName);
            var userRefershToken = new UserRefreshToken
            {
                AddedTime = DateTime.Now,
                ExpiryDate= DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
                IsUsed= true,
                IsRevoked=false,
                JWtId= JwtToken.Id,
                RefreshToken=refershToken.TokenString,
                Token=accessToken,
                UserId=user.Id, 

            };
            await _refershTokenRepository.AddAsync(userRefershToken);
           
            var response = new JwtAuthResult();
            response.refreshToken= refershToken;
            response.AccessToken = accessToken;
            return response; 
           
        }
       
        private RefreshToken GetRefreshToken(string username)
        {
            var refreshToken = new RefreshToken
            {
                ExpireAt = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
                UserName = username,
                TokenString = GenerateRefershToken()

            };
           
            return refreshToken;
        }
        private string GenerateRefershToken()
        {
            var randomNumber = new byte[32];
            var randomNumberGenerate = RandomNumberGenerator.Create();
            randomNumberGenerate.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
       

        private async Task<(JwtSecurityToken,string)> GenerateJWTToken(User user)
        {
            var roles=await _userManager.GetRolesAsync(user);
            var claims = GetClaims(user,roles.ToList());
            var JwtToken = new JwtSecurityToken(
                _jwtSettings.Issuser, _jwtSettings.Audience,
                claims, expires: DateTime.UtcNow.AddDays(_jwtSettings.AccessTokenExpireDate),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(JwtToken);
            return (JwtToken, accessToken);
        }
        public List<Claim> GetClaims(User user, List<string> roles)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserName),
                new Claim(nameof(UserClaimModel.PhoneNumber), user.PhoneNumber),
                new Claim(nameof(UserClaimModel.Id), user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Name,user.UserName)
                
            };
            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role,role));
            }  
            return claims;
        }
        public async Task<JwtAuthResult> GetRefreshToken(User user,JwtSecurityToken token,DateTime? expiryDate,string refreshToken)
        {
            var (jwtSecurityToken, newToken) =await GenerateJWTToken(user);
            
            //token is expire or not
            //generate refreshtoken
            var response = new JwtAuthResult();
            
            response.AccessToken = newToken;
            var refreshTokenResult = new RefreshToken();
            refreshTokenResult.UserName= token.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.UserName)).Value;
            refreshTokenResult.TokenString = refreshToken;
            refreshTokenResult.ExpireAt = (DateTime)expiryDate;
            response.refreshToken = refreshTokenResult;
            return response;    


        }
        public JwtSecurityToken ReadJWTToken(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentNullException(nameof(accessToken));
            }
            var handler = new JwtSecurityTokenHandler();
            var response = handler.ReadJwtToken(accessToken);
            return response;
                
        }

        public async Task<string> ValidateToken(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
     
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = _jwtSettings.ValidateIssuser,
                ValidIssuers = new[] { _jwtSettings.Issuser },
                ValidateIssuerSigningKey = _jwtSettings.ValidateIssuserSigingKey,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
                ValidAudience = _jwtSettings.Audience,
                ValidateAudience = _jwtSettings.ValidateAudience,
                ValidateLifetime = _jwtSettings.ValidateLifeTime,
            };
            try
            {
                var validator = handler.ValidateToken(accessToken, parameters, out SecurityToken validatedToken);
            
                if (validator == null)
                {
                   return "InvalidToken";

                }
                return "NotExpired";

            }
            catch (Exception ex) 
            {
                return ex.Message;
            }

        }

        public async Task<(string,DateTime?)> ValidateDetails(JwtSecurityToken token, string AccessToken, string RefreshToken)
        {
            if (token == null || !token.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
            {
              
                return ("AlgrothimIsWrong",null);
            }
            if (token.ValidTo > DateTime.UtcNow)
            {
                
                return ("TokenIsNotExpired", null);
            }
            //get user
           
             var userId = token.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.Id)).Value;
            var userRefreshToken =  _refershTokenRepository.GetTableNoTracking()
                                              .FirstOrDefault(x => x.Token == AccessToken &&
                                                                      x.RefreshToken == RefreshToken
                                                                      && x.UserId == int.Parse(userId));
            if (userRefreshToken == null)
            {
                return("RefreshTokenIsNotFound",null);
            }
            //validations token,refreshtoken
            if (userRefreshToken.ExpiryDate < DateTime.UtcNow)
            {
                userRefreshToken.IsRevoked = true;
                userRefreshToken.IsUsed = false;
                await _refershTokenRepository.UpdateAsync(userRefreshToken);
                return(" RefreshTokenIsExpired",null);
            }
            var expiredate= userRefreshToken.ExpiryDate;
          
            return (userId, expiredate);
        }
        #endregion

    }

}

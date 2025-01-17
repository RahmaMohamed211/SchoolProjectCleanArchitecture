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
using static SchoolProject.Data.Helpers.JwtAuthResult;
using System.Security.Cryptography;
using SchoolProject.infrastructure.Abstract;
using Azure.Core;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

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
           
          var(JwtToken, accessToken) =  GenerateJWTToken(user);
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
        public List<Claim> GetClaims(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(nameof(UserClaimModel.UserName),user.UserName),
                new Claim(nameof(UserClaimModel.Email),user.Email),
                new Claim(nameof(UserClaimModel.PhoneNumber),user.PhoneNumber),
                new Claim(nameof(UserClaimModel.Id),user.Id.ToString()),
            };
            return claims;
        }

        private (JwtSecurityToken,string) GenerateJWTToken(User user)
        {
            var claims = GetClaims(user);
            var JwtToken = new JwtSecurityToken(
                _jwtSettings.Issuser, _jwtSettings.Audience,
                claims, expires: DateTime.UtcNow.AddDays(_jwtSettings.AccessTokenExpireDate),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(JwtToken);
            return (JwtToken, accessToken);
        }

        public async Task<JwtAuthResult> GetRefreshToken(string accessToken, string refreshToken)
        {
            //read token to get cliams
            var token = ReadJWTToken(accessToken);
            if (token == null ||!token.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
            {
                throw new SecurityTokenException("Algrothim is wrong");
            }
            if (token.ValidTo > DateTime.UtcNow)
            {
                throw new SecurityTokenException("Token is not Expired");
            }
            //get user
            var userId = 2;
          //  var userId = token.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.Id)).Value;
           var userRefreshToken= await _refershTokenRepository.GetTableNoTracking()
                                             .FirstOrDefaultAsync(x=>x.Token==accessToken&&
                                                                     x.RefreshToken==refreshToken
                                                                     && x.UserId==4);
            if (userRefreshToken == null) 
            {
                throw new SecurityTokenException("Refresh Token is Not found");
            }
            //validations token,refreshtoken
            if (userRefreshToken.ExpiryDate < DateTime.UtcNow)
            {
                userRefreshToken.IsRevoked = true;
                userRefreshToken.IsUsed = false; 
                await _refershTokenRepository.UpdateAsync(userRefreshToken);
                throw new SecurityTokenException(" Refresh Token is  Expired");
            }
            var user= await _userManager.FindByIdAsync("4");
            if (user == null)
            {
                throw new SecurityTokenException("user is not found");  
            }
            var (jwtSecurityToken, newToken) = GenerateJWTToken(user);
            
            //token is expire or not
            //generate refreshtoken
            var response = new JwtAuthResult();
            
            response.AccessToken = newToken;
            var refreshTokenResult = new RefreshToken();
            refreshTokenResult.UserName= token.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.UserName)).Value;
            refreshTokenResult.TokenString = refreshToken;
            refreshTokenResult.ExpireAt = userRefreshToken.ExpiryDate;
            response.refreshToken = refreshTokenResult;
            return response;    


        }
        private JwtSecurityToken ReadJWTToken(string accessToken)
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
            var validator = handler.ValidateToken(accessToken, parameters, out SecurityToken validatedToken);
            try
            {
                if (validator == null)
                {
                    throw new SecurityTokenExpiredException("Invalid token");

                }
                return "NotExpired";

            }
            catch (Exception ex) 
            {
                return ex.Message;
            }

        }
        #endregion

    }

}

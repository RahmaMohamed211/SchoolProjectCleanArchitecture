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

namespace SchoolProject.Service.Implementations
{
    public class AuthenicationService : IAuthenicationService
    {

        #region fields
       
        private readonly JwtSettings _jwtSettings;
        private readonly IRefershTokenRepository _refershTokenRepository;
        private readonly ConcurrentDictionary<string, RefreshToken> _userRefreshToken;
        #endregion
        #region ctor
        public AuthenicationService(JwtSettings jwtSettings,IRefershTokenRepository refershTokenRepository)
        {
            
            _jwtSettings = jwtSettings;
            _refershTokenRepository = refershTokenRepository;
            _userRefreshToken = new ConcurrentDictionary<string, RefreshToken>();
        }

        #endregion
        #region function
        public async Task< JwtAuthResult> GetJWTToken(User user)
        {
            var claims= GetClaims(user);
            var JwtToken = new JwtSecurityToken(
                _jwtSettings.Issuser,_jwtSettings.Audience,
                claims, expires: DateTime.UtcNow.AddDays(_jwtSettings.AccessTokenExpireDate),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature));
            var accessToken= new JwtSecurityTokenHandler().WriteToken(JwtToken);
            var refershToken= GetRefreshToken(user.UserName);
            var userRefershToken = new UserRefreshToken
            {
                AddedTime = DateTime.Now,
                ExpiryDate= DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
                IsUsed= false,
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
            _userRefreshToken.AddOrUpdate(refreshToken.TokenString, refreshToken, (s, t) => refreshToken);
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
            };
            return claims;
        } 
        #endregion

    }

}

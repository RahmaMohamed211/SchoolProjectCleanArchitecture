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

namespace SchoolProject.Service.Implementations
{
    public class AuthenicationService : IAuthenicationService
    {

        #region fields
       
        private readonly JwtSettings _jwtSettings;
        #endregion
        #region ctor
        public AuthenicationService(JwtSettings jwtSettings)
        {
            
            _jwtSettings = jwtSettings;
        }

        #endregion
        #region function
        public Task<string> GetJWTToken(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(nameof(UserClaimModel.UserName),user.UserName),
                new Claim(nameof(UserClaimModel.Email),user.Email),
                new Claim(nameof(UserClaimModel.PhoneNumber),user.PhoneNumber),
            };
            var AccessToken = new JwtSecurityToken(
                _jwtSettings.Issuser,_jwtSettings.Audience,
                claims, expires: DateTime.UtcNow.AddMinutes(20),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature));
            var accessToken= new JwtSecurityTokenHandler().WriteToken(AccessToken);
            return Task.FromResult(accessToken);
        }
        #endregion

    }
}

using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Results;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service.Abstracts
{
    public interface IAuthenicationService
    {
        public Task< JwtAuthResult> GetJWTToken(User user);
        public JwtSecurityToken ReadJWTToken(string accessToken);
        public Task<(string,DateTime?)> ValidateDetails(JwtSecurityToken token ,string AccessToken,string RefreshToken);
        public Task<JwtAuthResult> GetRefreshToken(User user, JwtSecurityToken token, DateTime? expiryDate, string refreshToken);

        public Task<string> ValidateToken(string AccessToken);

        public Task<string> ConfirmEmail(int? userId,string? code);
        public Task<string> SendResetPasswordCode(string Email);
        public Task<string> ConfirmResetPassword(string Code,string Email);
        public Task<string> ResetPassword(string Email,string Password);
    }
}

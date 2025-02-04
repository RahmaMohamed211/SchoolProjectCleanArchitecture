using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using SchoolProject.Service.AuthService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service.AuthService.Implementations
{
    public class CurrentUserService : ICurrentUserService
    {

        #region fields
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;
        #endregion
        #region ctor
        public CurrentUserService(IHttpContextAccessor httpContextAccessor,UserManager<User> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        #endregion
        #region fuctions
        public int GetuserId()
        {
            var userId=_httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(claim=>claim.Type==nameof(UserClaimModel.Id)).Value;
            if(userId==null)
            {
                throw new UnauthorizedAccessException();  
            }
            return int.Parse( userId);
        }
        public async  Task<User> GetuserAsync()
        {
            var userId = GetuserId();
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }
            return user;
        }

        public async Task<List<string>> GetCurrentusersRolesAsync()
        {
            var user= await GetuserAsync();
            var roles= await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }


        #endregion

    }
}

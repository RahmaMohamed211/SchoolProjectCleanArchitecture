using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service.Implementations
{

    public class AuthorizationService:Abstracts.IAuthorizationService
    {
      

        #region fields
        private readonly RoleManager<Role> _roleManager;

        #endregion
        #region ctor
        public AuthorizationService( RoleManager<Role> roleManager)
        {
           
            _roleManager = roleManager;
          
        }
        #endregion
        #region function
        public async Task<string> AddRoleAsync(string roleName)
        {
            var identityRole= new Role();
            identityRole.Name=roleName;
            var result= await _roleManager.CreateAsync(identityRole);
            if (result.Succeeded)
                return "success";
            return "Failed";
        }

        public async Task<bool> IsRoleExist(string roleName)
        {
           //var role=await _roleManager.FindByNameAsync(roleName);
           // if (role == null) return false;
           // return true;

            return await _roleManager.RoleExistsAsync(roleName);   
        }
        #endregion
    }
}

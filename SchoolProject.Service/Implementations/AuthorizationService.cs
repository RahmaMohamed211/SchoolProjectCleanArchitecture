using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using SchoolProject.Data.DTOs;
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
        private readonly UserManager<User> _userManager;

        #endregion
        #region ctor
        public AuthorizationService( UserManager<User> userManager,RoleManager<Role> roleManager)
        {
            _userManager = userManager;
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

       

        public async Task<bool> IsRoleExistByName(string roleName)
        {
           //var role=await _roleManager.FindByNameAsync(roleName);
           // if (role == null) return false;
           // return true;

            return await _roleManager.RoleExistsAsync(roleName);   
        }
        public async Task<string> EditRoleAsync(EditRoleRequest request)
        {
            //check role is exist or not
            var role = await _roleManager.FindByIdAsync(request.Id.ToString());
            //if not exist return notfound
            if (role == null)
            {
                return "NotFound";
            }
            role.Name = request.Name;
            role.NormalizedName = request.Name.ToUpper();
            //edit
            //success
            var result = await _roleManager.UpdateAsync(role); 
            if (result.Succeeded) return "success";
            var errors = string.Join("_", result.Errors);
            return errors;
           
        }

        public async Task<string> DeleteRoleAsync(int roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null) return "NotFound";
            //check if user has this role or not
            var users = await _userManager.GetUsersInRoleAsync(role.Name);
            //return exception
            if (users != null && users.Count()>0) return "Used";
           //delete
           var result=  await _roleManager.DeleteAsync(role);
            //success
            if (result.Succeeded) return "Success";
            var errors = string.Join("_", result.Errors);
            return errors;


        }

        public async Task<bool> IsRoleExistById(int roleId)
        {
            var role= await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null) return false;
            else return true;
            
        }
        #endregion
    }
}

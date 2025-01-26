using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using SchoolProject.Data.Requests;
using SchoolProject.Data.Results;
using SchoolProject.infrastructure.Data;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service.Implementations
{

    public class AuthorizationService:Abstracts.IAuthorizationService
    {
        #region fields
        private readonly RoleManager<Role> _roleManager;
        private readonly APPDBContext _appContext;
        private readonly UserManager<User> _userManager;

        #endregion
        #region ctor
        public AuthorizationService(APPDBContext appContext ,UserManager<User> userManager,RoleManager<Role> roleManager)
        {
            _appContext = appContext;
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

        public async Task<List<Role>> GetRolesList()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return roles;
        }

        public async Task<Role> GetRoleById(int id)
        {
            return await _roleManager.FindByIdAsync(id.ToString());
            
        }



        public async Task<ManageUserRoleResult> ManageUserRolesData(User user)
        {
            var response= new ManageUserRoleResult();
            var RolesList=new List<Roles>();
            //userroles
            var userRoles =await _userManager.GetRolesAsync(user);
            //Roles
            var roles = await _roleManager.Roles.ToListAsync();
             response.UserId = user.Id;
            //if roles contain userroles true false
            //return
            foreach (var role in roles)
            {
                var userRole=new Roles();
                userRole.Id=role.Id;
                userRole.Name=role.Name;
                if (userRoles.Contains(role.Name))
                {
                    userRole.HasRole=true;

                }
                else
                {
                    userRole.HasRole = false;
                }
                RolesList.Add(userRole);
            }
            response.Roles = RolesList;
            return response;
           
        }

        public async Task<string> UpdateUserRoles(UpdareUserRoleRequest request)
        {
            var transact = await _appContext.Database.BeginTransactionAsync();
            try
            {


                //get user
                var user = await _userManager.FindByIdAsync(request.UserId.ToString());
                if (user == null)
                {
                    return "UserIsNull";
                }
                //get user oldRoles
                var userRoles = await _userManager.GetRolesAsync(user);
                //delete oldRoles
                var Removeresult = await _userManager.RemoveFromRolesAsync(user, userRoles);
                if (!Removeresult.Succeeded)
                {
                    return "FailedToRemoveOldRoles";
                }
                var selectedRoles = request.Roles.Where(x => x.HasRole == true).Select(x => x.Name);
                //add the roles hasrole=true
                var AddRolesresult = await _userManager.AddToRolesAsync(user, selectedRoles);
                if (!AddRolesresult.Succeeded) return "FialedToAddNewRoles";
                  await transact.CommitAsync();
                //retun result
                return "Success";
            }
            catch (Exception ex) 
            {
                await transact.RollbackAsync();
                return "FailedToUpdateuserRoles";
            }

           }

        public async Task<ManageUserClaimResult> ManageUserClaimsData(User user)
        {
            var response= new ManageUserClaimResult();
            var userClaimsList = new List<UserClaims>();
            response.UserId = user.Id;
            //get user claims
            var UserClaims= await _userManager.GetClaimsAsync(user);//edit
                                                                     //create edit get print
            foreach (var claim in ClaimStore.claims)
            {
                var userClaim = new UserClaims();
               userClaim.Type = claim.Type;
                if (UserClaims.Any(x=>x.Type==claim.Type))
                {
                    userClaim.Value= true;

                }
                else
                {
                    userClaim.Value = false;
                }
               userClaimsList.Add(userClaim);

            }
            response.userClaims = userClaimsList;
            //check if claim exist for user then value=true
            //return result
            return response;
        }

        public async Task<string> UpdateUserClaims(UpdateUserClaimsRequest request)
        {
            var transact = await _appContext.Database.BeginTransactionAsync();
            try
            {
                var user= await _userManager.FindByIdAsync(request.UserId.ToString());
                if (user == null)
                {
                    return "UserIsNull";
                }
                //remove old claims
                var UserClaims = await _userManager.GetClaimsAsync(user);
                var RemoveClaimsresult = await _userManager.RemoveClaimsAsync(user, UserClaims);
                if (!RemoveClaimsresult.Succeeded)
                {
                    return "FailedToRemoveOldClaim";
                }
                var claims=request.userClaims.Where(x=>x.Value==true).Select(x=>new Claim(x.Type,x.Value.ToString()));

                var updateUserClaimResult =await _userManager.AddClaimsAsync(user, claims);
                if (!updateUserClaimResult.Succeeded) return "FialedToAddNewClaims";
                await transact.CommitAsync();
                return "Success";
               
            }
            catch (Exception ex)
            {
                await transact.RollbackAsync();
                return "FailedToUpdateClaims";
            }

        }


        #endregion
    }
}

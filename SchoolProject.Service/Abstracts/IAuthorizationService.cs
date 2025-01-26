using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Results;
using SchoolProject.Data.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service.Abstracts
{
    public interface IAuthorizationService
    {
        public Task<string> AddRoleAsync(string roleName);
        public Task<string> EditRoleAsync(EditRoleRequest request);
        public Task<string> DeleteRoleAsync(int roleId);
        public Task<bool> IsRoleExistByName(string roleName);
        public Task<bool> IsRoleExistById(int roleId);

        public Task<List<Role>> GetRolesList();
        public Task<Role> GetRoleById(int id);


        public Task<ManageUserRoleResult> ManageUserRolesData(User user);
        public Task<ManageUserClaimResult> ManageUserClaimsData(User user);

        public Task<string> UpdateUserRoles(UpdareUserRoleRequest request);
        public Task<string> UpdateUserClaims(UpdateUserClaimsRequest request);


    }
}

using SchoolProject.Core.Features.Authorization.Queries.Results;
using SchoolProject.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Mapping.Roles
{
    public partial class RoleProfile
    {
        public void GetRolesListMapping()
        {
         CreateMap<Role,GetRolesListResult>()
                .ForMember(dest=>dest.Id,opt=>opt.MapFrom(src=>src.Id))
                .ForMember(dest=>dest.Name,opt=>opt.MapFrom(src=>src.Name));

        }
    }
}

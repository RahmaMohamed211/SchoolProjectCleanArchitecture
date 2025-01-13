using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Mapping.ApplicationUser
{ 
    public partial class ApplicationUserProfile
    {
        public void UpdateUserMapping()
        {
            CreateMap<EditUserCommand, User>();
        }
    }
}

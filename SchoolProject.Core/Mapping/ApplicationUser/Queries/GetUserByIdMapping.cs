using AutoMapper;
using SchoolProject.Core.Features.ApplicationUser.Queries.Models;
using SchoolProject.Core.Features.ApplicationUser.Queries.Results;
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
        public void GetUserByIdMapping()
        {
            CreateMap<User,GetUserByIdResponse>();

        }
    }
}

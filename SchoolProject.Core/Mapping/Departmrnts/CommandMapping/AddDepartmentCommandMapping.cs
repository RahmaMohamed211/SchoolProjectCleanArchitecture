using SchoolProject.Core.Features.Departments.Commands.Models;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SchoolProject.Core.Mapping.Departmrnts
{
    public partial class DepartmentProfile
    {
        public void AddDepartmentCommandMapping()
        {
            CreateMap<AddDepartmentCommand, Department>()
               .ForMember(dest => dest.DNameEn, opt => opt.MapFrom(src => src.DNameEn))
               .ForMember(dest => dest.DNameAr, opt => opt.MapFrom(src => src.DNameAr));

        }
    }
}

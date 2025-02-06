using SchoolProject.Core.Features.Departments.Queries.Results;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Mapping.Departmrnts
{
    public partial class DepartmentProfile
    {
        public void GetDepartmentStudentCountMapping()
        {
            CreateMap<ViewDepartment, GetDepartmentStudentListCountResults>()
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.DNameAr, src.DNameEn)))
               .ForMember(dest => dest.StudentCount, opt => opt.MapFrom(src => src.studentcount)).ReverseMap();
        }
    }
}

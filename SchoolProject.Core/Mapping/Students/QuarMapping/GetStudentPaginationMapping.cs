using SchoolProject.Core.Features.Students.Queries.Results;
using SchoolProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Mapping.Students
{
    public partial class StudentProfile
    {
        public void GetStudentPaginationMapping()
        {
            CreateMap<Student, GetStudentPaginatedListResonse>()
               .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Localize(src.Department.DNameAr, src.Department.DNameEn)))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.NameAr, src.NameEn)))
               .ForMember(dest => dest.StudID, opt => opt.MapFrom(src => src.StudID))
               .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));


        }
    }
}

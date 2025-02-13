using SchoolProject.Core.Features.Instructors.Queries.Results;
using SchoolProject.Core.Features.Students.Queries.Results;
using SchoolProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Mapping.Instructors
{
    public partial class InstructorProfile
    {
        public void GetInstructorByIdMapping()
        {
            CreateMap<Instructor, GetInstructorsResponse>()
               .ForMember(dest => dest.DapartmertName, opt => opt.MapFrom(src => src.department.Localize(src.department.DNameAr, src.department.DNameEn)))
               .ForMember(dest => dest.EName, opt => opt.MapFrom(src => src.Localize(src.ENameAr, src.ENameEn)));
        }
    }
}

using SchoolProject.Core.Features.Subjects.Queries.Results;
using SchoolProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Mapping.Subjects
{
    public partial class SubjectProfile
    {
        public void GetSubjecByIdQueryMapping()
        {
            CreateMap<Subject, GetSubjecByIdResponse>()
              .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Localize(src.DepartmentSubjects.Select(m => m.Department.DNameAr).FirstOrDefault(), src.DepartmentSubjects.Select(s => s.Department.DNameEn).FirstOrDefault())))
              .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Localize(src.SubjectNameAr, src.SubjectNameEn)));
             

        }
    }
}

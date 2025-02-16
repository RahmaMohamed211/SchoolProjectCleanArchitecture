using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Features.Subjects.Commands.Models;
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
        public void AddSubjectCommandMapping()
        {
            CreateMap<AddSubjectCommand, Subject>()
              .ForMember(dest => dest.SubjectNameEn, opt => opt.MapFrom(src => src.SubjectNameEn))
              .ForMember(dest => dest.SubjectNameAr, opt => opt.MapFrom(src => src.SubjectNameAr))
              .ForMember(dest => dest.Period, opt => opt.MapFrom(src => src.Period));

        }
    }
}

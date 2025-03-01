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
        public void GetSubjectWithStudentMapping()
        {
            CreateMap<Subject, GetSubjectWithStudentResponse>()
            .ForMember(dest => dest.SubjectNameAr, opt => opt.MapFrom(src => src.SubjectNameAr))
            .ForMember(dest => dest.SubjectNameEn, opt => opt.MapFrom(src => src.SubjectNameEn))
            .ForMember(dest => dest.NameAr, opt => opt.MapFrom(src => src.StudentSubjects.Select(m => m.Student.NameAr).FirstOrDefault()))
            .ForMember(dest => dest.grade, opt => opt.MapFrom(src => src.StudentSubjects.Select(m => m.grade).FirstOrDefault()))
            .ForMember(dest => dest.NameEn, opt => opt.MapFrom(src => src.StudentSubjects.Select(m => m.Student.NameEn).FirstOrDefault()))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.StudentSubjects.Select(m => m.Student.Address).FirstOrDefault()))
            .ForMember(dest => dest.Period, opt => opt.MapFrom(src => src.Period))
            .ForMember(dest => dest.phone, opt => opt.MapFrom(src => src.StudentSubjects.Select(m => m.Student.Phone).FirstOrDefault()))
            .ForMember(dest => dest.departmentName, opt => opt.MapFrom(src => src.StudentSubjects.Select(m => m.Student.Department.DNameEn).FirstOrDefault()));

        }
    }
}

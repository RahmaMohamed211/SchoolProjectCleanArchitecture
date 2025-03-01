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
        public void GetSubjectWithInstructorQueyMapping()
        {
            CreateMap<Subject, GetSubjectWithInstructorResponse>()
             .ForMember(dest => dest.SubjectNameAr, opt => opt.MapFrom(src => src.SubjectNameAr))
             .ForMember(dest => dest.SubjectNameEn, opt => opt.MapFrom(src => src.SubjectNameEn))
             .ForMember(dest => dest.InsNameAr, opt => opt.MapFrom(src => src.Ins_Subjects.Select(m => m.instructor.ENameAr).FirstOrDefault()))
             .ForMember(dest => dest.InsNameEn, opt => opt.MapFrom(src => src.Ins_Subjects.Select(m => m.instructor.ENameEn).FirstOrDefault()))
             .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Ins_Subjects.Select(m => m.instructor.Address).FirstOrDefault()))
             .ForMember(dest => dest.position, opt => opt.MapFrom(src => src.Ins_Subjects.Select(m => m.instructor.Position).FirstOrDefault()))
             .ForMember(dest => dest.salary, opt => opt.MapFrom(src => src.Ins_Subjects.Select(m => m.instructor.Salary).FirstOrDefault()))
             .ForMember(dest => dest.departmentName, opt => opt.MapFrom(src => src.Ins_Subjects.Select(m => m.instructor.department.DNameEn).FirstOrDefault()));



        }
    }
}

using SchoolProject.Core.Features.Departments.Queries.Results;
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
         public void GetDepartmentByIdMapping()
        {
            CreateMap<Department, GetDepartmentByIDResponse>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.DNameAr, src.DNameEn)))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DId))
                .ForMember(dest => dest.ManagerName, opt => opt.MapFrom(src => src.Instructor.Localize(src.Instructor.ENameAr, src.Instructor.ENameEn)))
                .ForMember(dest => dest.SubjectList, opt => opt.MapFrom(src => src.DepartmentSubjects))
                //.ForMember(dest => dest.StudentList, opt => opt.MapFrom(src => src.Students))
                .ForMember(dest => dest.InstructorList, opt => opt.MapFrom(src => src.Instructors));

            CreateMap<DepartmentSubject, SubjectResponse>()
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SubID))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Subjects.Localize(src.Subjects.SubjectNameAr, src.Subjects.SubjectNameEn)));


            //CreateMap<Student, StudentResponse>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.StudID))
            //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.NameAr, src.NameEn)));

            CreateMap<Instructor, InstructorResponse>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.InsId))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.ENameAr, src.ENameEn)));

        }
    }
}

using SchoolProject.Core.Features.Instructors.Commands.Models;
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
        public void EditInstructorMapping()
        {
            CreateMap<EditInstructorCommand, Instructor>()
             .ForMember(dest => dest.Image, opt => opt.Ignore())
             .ForMember(dest => dest.ENameAr, opt => opt.MapFrom(src => src.NameAr))
             .ForMember(dest => dest.ENameEn, opt => opt.MapFrom(src => src.NameEn))
             .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
             .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position));

        }
    }
}

using SchoolProject.Core.Features.Departments.Queries.Models;
using SchoolProject.Core.Features.Departments.Queries.Results;
using SchoolProject.Data.Entities.Procedures;
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
        public void GetDepartmentStudentCountByIdMapping()
        {
            CreateMap<GetDepartmentStudentCountByIDQuery, DepartmentStudentCountProcParameters>()
               .ForMember(dest => dest.DID, opt => opt.MapFrom(src => src.DID));

            CreateMap<DepartmentStudentCountProc, GetDepartmentStudentCountByIDResult>()
                            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.DNameAr, src.DNameEn)))
               .ForMember(dest => dest.StudentCount, opt => opt.MapFrom(src => src.StudentCount));

        }
    }
}

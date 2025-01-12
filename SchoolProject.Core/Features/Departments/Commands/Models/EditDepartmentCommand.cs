using MediatR;
using SchoolProject.Core.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Departments.Commands.Models
{
    public class EditDepartmentCommand :IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string? DNameAr { get; set; }

        public string? DNameEn { get; set; }
    }
}

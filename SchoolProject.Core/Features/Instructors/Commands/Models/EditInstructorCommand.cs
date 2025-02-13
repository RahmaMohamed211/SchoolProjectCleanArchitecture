using MediatR;
using Microsoft.AspNetCore.Http;
using SchoolProject.Core.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Instructors.Commands.Models
{
    public class EditInstructorCommand:IRequest<Response<string>>
    {
        public int id { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public string? Address { get; set; }
        public string? Position { get; set; }
        public int? SupervisorId { get; set; }
        public decimal? Salary { get; set; }
        public IFormFile? Image { get; set; }
        public int DID { get; set; }
    }
}

using MediatR;
using SchoolProject.Core.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Instructors.Commands.Models
{
    public class DeleteInstructorCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }

        public DeleteInstructorCommand(int id)
        {
            Id = id;
        }
    
    }
}

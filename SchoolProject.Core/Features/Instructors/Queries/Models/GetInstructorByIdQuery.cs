using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Instructors.Queries.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Instructors.Queries.Models
{
    public class GetInstructorByIdQuery:IRequest<Response<GetInstructorsResponse>>
    {
        public int id { get; set; }
        public GetInstructorByIdQuery(int Id)
        {
            id = Id;
        }
    }
}

using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Subjects.Queries.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Subjects.Queries.Models
{
    public class GetSubjectWithInstructorQuey:IRequest<Response<GetSubjectWithInstructorResponse>>
    {
        public int ID { get; set; }
        public GetSubjectWithInstructorQuey(int id)
        {
            ID = id;
        }
    }
}

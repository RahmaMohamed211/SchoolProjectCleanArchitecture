
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
    public class GetSubjectByIdQuery: IRequest<Response<GetSubjecByIdResponse>>
    {
        public int Id { get; set; }
        public GetSubjectByIdQuery(int id)
        {
            Id = id;
        }
    }
}

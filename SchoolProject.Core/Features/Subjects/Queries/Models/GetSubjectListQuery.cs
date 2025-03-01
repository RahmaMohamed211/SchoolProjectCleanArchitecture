using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Queries.Results;
using SchoolProject.Core.Features.Subjects.Queries.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Subjects.Queries.Models
{
    public class GetSubjectListQuery : IRequest<Response<List<GetSubjectListResponse>>>
    {
    }
}

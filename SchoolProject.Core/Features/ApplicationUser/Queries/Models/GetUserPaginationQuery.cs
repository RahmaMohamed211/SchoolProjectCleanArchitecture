using MediatR;
using SchoolProject.Core.Features.ApplicationUser.Queries.Results;
using SchoolProject.Core.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.ApplicationUser.Queries.Models
{
    public class GetUserPaginationQuery:IRequest<PaginatedResult<GetUserPaginationResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        
    }
}

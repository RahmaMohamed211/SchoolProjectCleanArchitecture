﻿using MediatR;
using SchoolProject.Core.Features.Students.Queries.Results;
using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Students.Queries.Models
{
    public class GetStudentPaginatedListQuery:IRequest<PaginatedResult<GetStudentPaginatedListResonse>>
    {
        public int PageNumer { get; set; }

        public int PageSize { get; set; }

        public StudentOrderingEnum OrderBy { get; set; }

        public string? Search { get; set; }
    }
}

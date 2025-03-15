using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Entities;
using SchoolProject.XUnitTest.Wrappers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.XUnitTest.Wrappers.Implementation
{
    public class PaginatedService : IPaginatedService<Student>
    {
        public async Task<PaginatedResult<Student>> ReturnPaginatedResult(IQueryable<Student> source, int PageNumber, int PageSize)
        {
            return await source.ToPaginatedListAsync(PageNumber, PageSize);
        }
    }
}

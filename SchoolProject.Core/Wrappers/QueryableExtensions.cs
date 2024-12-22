using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Wrappers
{
    public static class QueryableExtensions
    {   
        //                                                                           student     ,2           ,20 -دول الى هفلتر بيهم
        public static async Task<PaginatedResult<T>> ToPaginatedListAsync<T>(this IQueryable<T> source,int PageNumber,int PageSize) where T : class
        {
            //لو مش باعت حاجه يعنى qurable==null
            if (source == null) 
                throw new Exception("Empty");
            //defult 
            PageNumber =PageNumber==0 ? 1 : PageNumber; 
            PageSize=PageSize==0 ? 10 : PageSize;
            //بيحسب عدد حاجه عشان يشوف توتال كونت
            int count= await source.AsNoTracking().CountAsync();
            //معندهوش ولا ريكورد
            if (count == 0) return PaginatedResult<T>.Success(new List<T>(),count,PageNumber,PageSize);
            //لو اقل من صفر خلى 1 الا لو في رقم
            PageNumber=PageNumber<=0 ? 1 : PageNumber; 
            //page size=1 يروح ل داتا بيز  //
            var items=await source.Skip((PageNumber-1)*PageSize).Take(PageSize).ToListAsync();
            return PaginatedResult<T>.Success(items,count,PageNumber,PageSize);



        }
    }
}

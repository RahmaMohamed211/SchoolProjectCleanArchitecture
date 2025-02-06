using SchoolProject.Data.Results;
using SchoolProject.infrastructure.Abstract.Functions;
using StoredProcedureEFCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.infrastructure.Repositieries.Functions
{
    public class InstructorFunctionsRepository : IInstructorFunctionsRepository
    {
        #region fields

        #endregion

        #region ctor
        public InstructorFunctionsRepository()
        {
            
        }
        #endregion

        #region FUNCTIONS
        public async Task< decimal> GetSalarySummationOfInstructor(string query, DbCommand cmd)
        {
            decimal response = 0;
            cmd.CommandText = query;
            var reader= await cmd.ExecuteReaderAsync();
            var value =await reader.ToListAsync<GetInstructorFunctionResult>();
            var result = reader.ToString();
            if(decimal.TryParse(result,out decimal d))
            {
               response = d;
            }
            return response;
        } 
        #endregion
    }
}

using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Results;
using SchoolProject.infrastructure.Abstract.Functions;
using SchoolProject.infrastructure.Data;
using StoredProcedureEFCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.infrastructure.Repositieries.Functions
{
    public class InstructorFunctionsRepository : IInstructorFunctionsRepository
    {

        #region fields
        private readonly APPDBContext _context;
        #endregion

        #region ctor
        public InstructorFunctionsRepository(APPDBContext context)
        {
            _context = context;
        }
        #endregion

        #region FUNCTIONS
        public async Task<decimal> GetSalarySummationOfInstructor(string query)
        {
            using (var cmd = _context.Database.GetDbConnection().CreateCommand())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }
                //read from list
               // var reader =await cmd.ExecuteReaderAsync();
                //var value=await reader.ToListAsync<GetInstructorFunctionResult>();

                decimal response = 0;
                cmd.CommandText = query;
                var reader = await cmd.ExecuteReaderAsync();
                var value = await reader.ToListAsync<GetInstructorFunctionResult>();
                var result = reader.ToString();
                if (decimal.TryParse(result, out decimal d))
                {
                    response = d;
                }
                cmd.Connection.CloseAsync();
                return response;
            }
            #endregion
        }
    }
}

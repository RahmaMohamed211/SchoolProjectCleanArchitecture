using Microsoft.EntityFrameworkCore;
using SchoolProject.infrastructure.Data;
using SchoolProject.Service.Abstracts;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolProject.infrastructure.Abstract.Functions;

namespace SchoolProject.Service.Implementations
{
    public class InstructorService:IInstructorService
    {

        #region fields
        private readonly APPDBContext _context;
        private readonly IInstructorFunctionsRepository _instructorFunctionsRepository;
        #endregion
        #region ctor
        public InstructorService(APPDBContext context, IInstructorFunctionsRepository instructorFunctionsRepository)
        {
            _context = context;
            _instructorFunctionsRepository = instructorFunctionsRepository;
        }


        #endregion
        #region functions
        public async Task<decimal> GetSalarySummationOfInstructor()
        {
            decimal result = 0;
            using(var cmd = _context.Database.GetDbConnection().CreateCommand())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }
                result =await _instructorFunctionsRepository.GetSalarySummationOfInstructor("select * from dbo. GetInstructorData()", cmd);

            }
            return  result;
        }
        #endregion
    }
}

using SchoolProject.Data.Entities.Procedures;
using SchoolProject.infrastructure.Abstract.Procedures;
using SchoolProject.infrastructure.Data;
using StoredProcedureEFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.infrastructure.Repositieries.Procedures
{
    public class DepartmentStudentCountProcRepository : IDepartmentStudentCountProcRepository
    {

        #region fields
        private readonly APPDBContext _context;
        #endregion
        #region ctor
        public DepartmentStudentCountProcRepository(APPDBContext context)
        {
            _context = context;
        }
        #endregion
        #region functions
        public async Task<IReadOnlyList<DepartmentStudentCountProc>> GetDepartmentStudentCountProcAsync(DepartmentStudentCountProcParameters parameters)
        {
            var rows= new List<DepartmentStudentCountProc>();
          await  _context.LoadStoredProc(nameof(DepartmentStudentCountProc))
                .AddParam(nameof(DepartmentStudentCountProcParameters.DID),parameters.DID)
                  .ExecAsync(async r=>rows=await r.ToListAsync<DepartmentStudentCountProc>());
            return rows;
        }
        #endregion

    }
}

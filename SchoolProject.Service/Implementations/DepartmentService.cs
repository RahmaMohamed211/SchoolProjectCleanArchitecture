using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.infrastructure.Abstract;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service.Implementations
{
    internal class DepartmentService : IDepartmentService
    {
        #region fields
        private readonly IDepartmentRepository _departmentRepo;
        #endregion
        #region ctor
        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepo = departmentRepository;
        }
        #endregion
        #region handleFunction
        public async Task<Department> GetDepartmentByID(int id)
        {
          var student= await  _departmentRepo.GetTableNoTracking().Where(x => x.DId.Equals(id))
                .Include(x => x.DepartmentSubjects).ThenInclude(x=>x.Subjects)
                
                .Include(x => x.Instructors)
                .Include(x=>x.Instructor).FirstOrDefaultAsync();

            return student;

        }
        #endregion

    }
}

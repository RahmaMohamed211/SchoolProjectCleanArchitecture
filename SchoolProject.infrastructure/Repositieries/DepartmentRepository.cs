using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.infrastructure.Abstract;
using SchoolProject.infrastructure.Data;
using SchoolProject.infrastructure.InfastructureBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.infrastructure.Repositieries
{
    public class DepartmentRepository:GenericRepositoryAsync<Department>,IDepartmentRepository
    {
        #region fields
        private DbSet<Department> departments;
        #endregion

        #region Ctor

        public DepartmentRepository(APPDBContext dbContext):base(dbContext) 
        {
            departments=dbContext.Set<Department>();
         }
        #endregion
        #region handelFunction

        #endregion
    }
}

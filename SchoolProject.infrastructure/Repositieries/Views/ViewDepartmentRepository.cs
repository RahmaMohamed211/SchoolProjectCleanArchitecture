using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Views;
using SchoolProject.infrastructure.Abstract.Views;
using SchoolProject.infrastructure.Data;
using SchoolProject.infrastructure.InfastructureBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.infrastructure.Repositieries.Views
{
    public class ViewDepartmentRepository:GenericRepositoryAsync<ViewDepartment>,IViewRepository<ViewDepartment>
    {

        #region fields
        private DbSet<ViewDepartment> viewDepartment;
        #endregion
        #region ctor
        public ViewDepartmentRepository(APPDBContext context):base(context) 
        {
            viewDepartment=context.Set<ViewDepartment>();
        }
        #endregion
        #region function

        #endregion
    }
}

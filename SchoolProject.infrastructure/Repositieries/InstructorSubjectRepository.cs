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
    public class InstructorSubjectRepository : GenericRepositoryAsync<Ins_Subject>, IInstructorSubjectRepository
    {
        #region fields
        private DbSet<Ins_Subject> insSubject;
        #endregion
        #region ctor
        public InstructorSubjectRepository(APPDBContext dbContext) : base(dbContext)
        {
            insSubject = dbContext.Set<Ins_Subject>();
        }
        #endregion
        #region function

        #endregion
    }
 
}

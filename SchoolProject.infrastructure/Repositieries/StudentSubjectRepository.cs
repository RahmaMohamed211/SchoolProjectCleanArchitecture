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
    public class StudentSubjectRepository : GenericRepositoryAsync<StudentSubject>, IStudentSubjectRepository
    {
        #region fields
        private DbSet<StudentSubject> studentSubjects;
        #endregion
        #region ctor
        public StudentSubjectRepository(APPDBContext dbContext) : base(dbContext)
        {
            studentSubjects = dbContext.Set<StudentSubject>();
        }
        #endregion
        #region function
        public async Task<StudentSubject> GetStudentSubject(int StdID, int SubId)
        {
            return await studentSubjects.FirstOrDefaultAsync(e => e.StudID == StdID && e.SubID == SubId);
        }


        #endregion
    }
}

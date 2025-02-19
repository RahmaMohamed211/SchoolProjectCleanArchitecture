﻿using Microsoft.EntityFrameworkCore;
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
    public class SubjectRepository : GenericRepositoryAsync<Subject>, ISubjectRepository
    {
        #region fields
        private DbSet<Subject> Subjects;
        #endregion

        #region Ctor

        public SubjectRepository(APPDBContext dbContext) : base(dbContext)
        {
            Subjects = dbContext.Set<Subject>();
        }


        #endregion
        #region handelFunction
        public async Task<Subject>? GetByIdWithInstructor(int Id)
        {
            var result = await _dbContext.Subjects.Include(s => s.Ins_Subjects)
                 .ThenInclude(s => s.instructor).FirstOrDefaultAsync(s => s.SubID == Id);
            return result;
        }

        public async Task<Subject>? GetByIdWithStudents(int Id)
        {
            var result= await _dbContext.Subjects.Include(s=>s.StudentSubjects)
                .ThenInclude(s=>s.Student).FirstOrDefaultAsync(s=>s.SubID == Id);
            return result;
        }
        #endregion
    }
}

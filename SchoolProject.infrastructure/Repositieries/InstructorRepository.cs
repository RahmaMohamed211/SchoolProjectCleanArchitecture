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
    public class InstructorRepository : GenericRepositoryAsync<Instructor>, IInstructorRepository
    {
        #region fields
        private DbSet<Instructor> Instructors;
        #endregion

        #region Ctor

        public InstructorRepository(APPDBContext dbContext) : base(dbContext)
        {
            Instructors = dbContext.Set<Instructor>();
        }
        #endregion
        #region handelFunction

        #endregion
    }
  
}
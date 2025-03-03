﻿using SchoolProject.Data.Entities;
using SchoolProject.infrastructure.InfastructureBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.infrastructure.Abstract
{
    public interface IInstructorRepository :IGenericRepositoryAsync<Instructor>
    {
        public Task<List<Instructor>> GetInstructorAsync();
        //public Task<Instructor> GetInstructorByIdAsync(int id);
    }
}

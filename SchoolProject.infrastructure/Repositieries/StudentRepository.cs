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
    public class StudentRepository :GenericRepositoryAsync<Student>, IStudentRepository
    {
        #region Fields
        private readonly DbSet<Student> _studentRepo;
        #endregion
        #region Constructors
        public StudentRepository(APPDBContext dbContext):base(dbContext) 
        {
            _studentRepo=dbContext.Set<Student>();
        }
        #endregion
        #region Handles functions
        public async Task<List<Student>> GetStudentAsync()
        {
            return await _studentRepo.Include(x=>x.Department).ToListAsync();
        }
        #endregion


    }
}

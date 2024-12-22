using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.infrastructure.Data
{
    public class APPDBContext :DbContext
    {
        public APPDBContext()
        {
            
        }

        public APPDBContext(DbContextOptions<APPDBContext> options) : base(options)
       
        {
            
        }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<DepartmentSubject> DepartmentSubjects { get; set; }    

        public DbSet<Subject> Subjects { get; set; }    


        public DbSet<StudentSubject> StudentSubjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
       

    }
}

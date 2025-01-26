using SchoolProject.Data.Entities;
using SchoolProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service.Abstracts
{
    public interface IStudentService
    {
        public Task<List<Student>> GetStudentsListAsync();

        public Task<Student> GetStudentByIDwithIncludeAsync(int id);

        public Task<Student> GetByIDAsync(int id);

        public  Task<string> AddAsync(Student student);

        public Task<bool> IsNameExist (string name);

        public Task<bool> IsNameExistExcludeSelf(string name,int id);

        public Task<string> EditAsync(Student student);

        public Task<string> DeleteAsync(Student student);

        public IQueryable<Student> GetStudentsQuerable();
        public IQueryable<Student> GetStudentsByDepartmentIDQuerable(int DID);

        public IQueryable<Student> FilterStudentPaginatedQuerable(StudentOrderingEnum orderingEnum, string search );


    }
}

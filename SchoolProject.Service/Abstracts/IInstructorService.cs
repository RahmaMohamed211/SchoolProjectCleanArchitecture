using Microsoft.AspNetCore.Http;
using SchoolProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service.Abstracts
{
    public interface IInstructorService
    {
        public Task<decimal> GetSalarySummationOfInstructor();

        public Task<bool> IsNameArExist(string nameAr);
        public Task<bool> IsNameEnExist(string nameEn);

        public Task<bool> IsNameArExistExcludeSelf(string nameAr, int id);
        public Task<bool> IsNameEnExistExcludeSelf(string nameEn, int id);
        public Task<string> AddInstrucorAsync(Instructor instructor,IFormFile file);
      
        public Task<string> EditInstructorAsync(Instructor instructor, IFormFile? file);

        public Task<List<Instructor>> GetInstructorListAsync();
        public Task<Instructor> GetInstructorByIdAsyncwithInclude(int id);

        public Task<Instructor> GetByIDAsync(int id);
        public Task<string> DeleteAsync(Instructor instructor);
    }
}

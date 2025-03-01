using SchoolProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service.Abstracts
{
    public interface ISubjectService
    {
        public Task<bool> IsNameArExist(string nameAr);
        public Task<bool> IsNameEnExist(string nameEn);

        public Task<string> AddSubject(Subject subject);
        public Task<string> EditSubjectAsync(Subject subject);
        public Task<string> deleteSubject(Subject subject);

        public Task<Subject> GetByIDAsync(int id); 
        public Task<Subject> GetSubjectByIDAsyncWithInclude(int id); 
        public Task<List<Subject>> GetSubjectsListAsync();
        public Task<Subject>? GetSubjectByInstructorAsync(int id);
        public Task<Subject>? GetSubjectByStudnentAsync(int id);
        public Task<string> AddsubjectToStudent(StudentSubject studSubject);
        public Task<string> DeletesubjectToStudent(int SubId,int studentId);
        public Task<string> DeletesubjectToInstructor(int InsId, int SubjId);
        public Task<string> AddsubjectToInstructor(int InsId,int SubjId);
    }
}

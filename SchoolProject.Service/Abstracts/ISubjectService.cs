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

        public Task<Subject> GetByIDAsync(int id);
    }
}

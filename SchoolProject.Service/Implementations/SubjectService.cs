using SchoolProject.Data.Entities;
using SchoolProject.infrastructure.Abstract;
using SchoolProject.infrastructure.Repositieries;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;

namespace SchoolProject.Service.Implementations
{
    public class SubjectService : ISubjectService
    {

        #region fields
        private readonly ISubjectRepository _subjectRepository;
        #endregion
        #region ctor
        public SubjectService(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }
        #endregion

        #region funtion
        public async Task<string> AddSubject(Subject subject)
        {
         var result= await  _subjectRepository.AddAsync(subject);
            return "Success";
        }

        public async Task<Subject> GetByIDAsync(int id)
        {
           return await  _subjectRepository.GetByIdAsync(id);
        }

        public async Task<bool> IsNameArExist(string nameAr)
        {
            //check if the name is exist or not
            var subjectResult = _subjectRepository.GetTableAsTracking().Where(x => x.SubjectNameAr.Equals(nameAr)).FirstOrDefault();
            if (subjectResult == null) return false;
            return true;
        }

        public async Task<bool> IsNameEnExist(string nameEn)
        {
            //check if the name is exist or not
            var subjectResult = _subjectRepository.GetTableAsTracking().Where(x => x.SubjectNameEn.Equals(nameEn)).FirstOrDefault();
            if (subjectResult == null) return false;
            return true;
        }
        #endregion
    }
}

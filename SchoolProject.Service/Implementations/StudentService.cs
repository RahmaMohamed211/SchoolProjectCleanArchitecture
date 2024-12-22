using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Helpers;
using SchoolProject.infrastructure.Abstract;
using SchoolProject.infrastructure.Repositieries;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service.Implementations
{
    public class StudentService : IStudentService
    {
        #region fields
        private readonly IStudentRepository _studentRepository;

        #endregion
        #region Constructor
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

       
        #endregion
        #region handle functions
        public async Task<List<Student>> GetStudentsListAsync()
        {
         return await _studentRepository.GetStudentAsync();
        }

        public async Task<Student> GetStudentByIDwithIncludeAsync(int id)
        {
          //  var student =await _studentRepository.GetByIdAsync(id);
          var student =_studentRepository.GetTableNoTracking()
                .Include(x=>x.Department).Where(x=>x.StudID == id).FirstOrDefault();
            return student;
        }

        public async Task<string> AddAsync(Student student)
        {
            //check if the name is exist or not
            
            

            //Added student
            //if (student.StudID != null)
            //    student.StudID = 0;
           await  _studentRepository.AddAsync(student);
            return "success";
        }

        public async Task<bool> IsNameExist(string nameEn)
        {
            //check if the name is exist or not
            var studentResult = _studentRepository.GetTableAsTracking().Where(x => x.NameEn.Equals(nameEn)).FirstOrDefault();
            if (studentResult == null) return false;
            return true;
        }

        public async Task<bool> IsNameExistExcludeSelf(string nameEn,int id)
        {
            var studentResult = await _studentRepository.GetTableAsTracking().Where(x => x.NameEn.Equals(nameEn)&!x.StudID.Equals(id)).FirstOrDefaultAsync();
            if (studentResult == null) return false;
            return true;
        }

        public async Task<string> EditAsync(Student student)
        {
           await _studentRepository.UpdateAsync(student);
            return "success";
        }

        public async Task<string> DeleteAsync(Student student)
        {
            var trans = _studentRepository.BeginTransaction();
            try
            {
                await _studentRepository.DeleteAsync(student);
              await trans.CommitAsync();
                return "success";
            }
            catch
            {
                await trans.RollbackAsync();
                return "Falied";
            } 
           
        }

        public Task<Student> GetByIDAsync(int id)
        {
            var student = _studentRepository.GetByIdAsync(id);
            return student;
        }

        public IQueryable<Student> GetStudentsQuerable()
        {
            return _studentRepository.GetTableNoTracking().Include(x=>x.Department).AsQueryable();
        }

        public IQueryable<Student> FilterStudentPaginatedQuerable(StudentOrderingEnum orderingEnum, string search)
         {
            var querable= _studentRepository.GetTableNoTracking().Include(x => x.Department).AsQueryable();
            if (search != null)
            {
                querable = querable.Where(x => x.NameAr.Contains(search) || x.Address.Contains(search));
            }
            switch (orderingEnum)
            {
                case StudentOrderingEnum.StudID:
                    querable = querable.OrderBy(x => x.StudID);
                    break;
                case StudentOrderingEnum.Name:
                    querable = querable.OrderBy(x => x.NameAr);
                    break;
                case StudentOrderingEnum.Address:
                    querable = querable.OrderBy(x => x.Address);
                    break;
                case StudentOrderingEnum.DepartmentName:
                    querable = querable.OrderBy(x => x.Department.DNameAr);
                    break;
            }


            return querable;
        }

        public IQueryable<Student> GetStudentsByDepartmentIDQuerable(int DID)
        {
            return _studentRepository.GetTableNoTracking().Where(x=>x.DId.Equals(DID)).AsQueryable();
        }
        #endregion

    }
}

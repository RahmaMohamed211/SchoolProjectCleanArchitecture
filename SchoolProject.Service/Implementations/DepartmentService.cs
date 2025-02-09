using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Procedures;
using SchoolProject.Data.Entities.Views;
using SchoolProject.infrastructure.Abstract;
using SchoolProject.infrastructure.Abstract.Procedures;
using SchoolProject.infrastructure.Abstract.Views;
using SchoolProject.infrastructure.Repositieries;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service.Implementations
{
    internal class DepartmentService : IDepartmentService
    {
        #region fields
        private readonly IDepartmentRepository _departmentRepo;
        private readonly IDepartmentStudentCountProcRepository _departmentStudentCountProcRepository;
        private readonly IViewRepository<ViewDepartment> _viewRepository;
        #endregion
        #region ctor
        public DepartmentService(IDepartmentRepository departmentRepository
                             ,IDepartmentStudentCountProcRepository departmentStudentCountProcRepository,IViewRepository<ViewDepartment> viewRepository)
        {
            _departmentRepo = departmentRepository;
            _departmentStudentCountProcRepository = departmentStudentCountProcRepository;
            _viewRepository = viewRepository;
        }


        #endregion
        #region handleFunction
        public async Task<string> AddDepartmentAsync(Department department)
        {
            await _departmentRepo.AddAsync(department);
            return "success";
        }
        public async Task<bool> IsNameArExist(string nameAr)
        {
            //check if the name is exist or not
            var departmentResult = _departmentRepo.GetTableAsTracking().Where(x => x.DNameAr.Equals(nameAr)).FirstOrDefault();
            if (departmentResult == null) return false;
            return true;
        }
        public async Task<bool> IsNameEnExist(string nameEn)
        {
            //check if the name is exist or not
            var departmentResult = _departmentRepo.GetTableAsTracking().Where(x => x.DNameEn.Equals(nameEn)).FirstOrDefault();
            if (departmentResult == null) return false;
            return true;
        }
        public async Task<Department> GetDepartmentByID(int id)
        {
          var student= await  _departmentRepo.GetTableNoTracking().Where(x => x.DId.Equals(id))
                .Include(x => x.DepartmentSubjects).ThenInclude(x=>x.Subjects)
                
                .Include(x => x.Instructors)
                .Include(x=>x.Instructor).FirstOrDefaultAsync();

            return student;

        }
        public async Task<string> EditAsync(Department department)
        {
            await _departmentRepo.UpdateAsync(department);
            return "success";
        }
        public async Task<string> DeleteAsync(Department department)
        {
            var trans = await _departmentRepo.BeginTransactionAsync();
            try
            {

                await _departmentRepo.DeleteAsync(department);
                await trans.CommitAsync();
                return "success";
            }
            catch
            {
                await trans.RollbackAsync();
                return "Falied";
            }

        }
        public async Task<bool> IsDepartmentIdExist(int departmentId)
        {
            ///Any اى واحد فيهم يكون بيساوى id 
           return await _departmentRepo.GetTableNoTracking().AnyAsync(x=>x.DId.Equals(departmentId ));
        }
        public async Task<bool> IsNameExistEnExcludeSelf(string DepartmentEn, int id)
        {
            var studentResult = await _departmentRepo.GetTableAsTracking().Where(x => x.DNameEn.Equals(DepartmentEn) & !x.DId.Equals(id)).FirstOrDefaultAsync();
            if (studentResult == null) return false;
            return true;
        }
        public async Task<bool> IsNameExistArExcludeSelf(string DepartmentAr, int id)
        {
            var studentResult = await _departmentRepo.GetTableAsTracking().Where(x => x.DNameAr.Equals(DepartmentAr) & !x.DId.Equals(id)).FirstOrDefaultAsync();
            if (studentResult == null) return false;
            return true;
        }

        public async Task<List<ViewDepartment>> GetViewDepartmentDataAsync()
        {
            var viewDepartment= await _viewRepository.GetTableNoTracking().ToListAsync();
            return viewDepartment;
        }

        public async Task<IReadOnlyList<DepartmentStudentCountProc>> GetDepartmentStudentCountProcAsync(DepartmentStudentCountProcParameters parameters)
        {
            return await _departmentStudentCountProcRepository.GetDepartmentStudentCountProcAsync(parameters);
        }
        #endregion

    }
}

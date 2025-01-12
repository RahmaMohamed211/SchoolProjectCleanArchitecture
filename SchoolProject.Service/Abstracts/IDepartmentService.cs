using SchoolProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service.Abstracts
{
    public interface IDepartmentService
    {
        public Task<Department> GetDepartmentByID(int id);
        public Task<bool> IsDepartmentIdExist(int departmentId);

        public Task<string> AddDepartmentAsync(Department department);

        public Task<bool> IsNameArExist(string name);
        public Task<bool> IsNameEnExist(string name);
        public Task<string> EditAsync(Department department);

        public Task<bool> IsNameExistEnExcludeSelf(string DepartmentEn, int id);
        public Task<bool> IsNameExistArExcludeSelf(string DepartmentArn, int id);

        public Task<string> DeleteAsync(Department department);

    }
}

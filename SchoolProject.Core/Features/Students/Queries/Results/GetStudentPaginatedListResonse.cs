using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Students.Queries.Results
{
    public class GetStudentPaginatedListResonse
    {
        public int StudID { get; set; }

        public string? Name { get; set; }

        public string? Address { get; set; }
        public string? DepartmentName { get; set; }

        //public GetStudentPaginatedListResonse (int studId, string? name ,string? address ,string? departmentName )
        //{
        //    StudID = studId;
        //    Name = name;
        //    Address = address;
        //    DepartmentName = departmentName;

        //}
    }
}

using SchoolProject.Data.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Data.Entities.Procedures
{
    public class DepartmentStudentCountProc:GeneralLocalizableEntity
    {
        public int DId { get; set; }
        public string? DNameAr { get; set; }
        public string? DNameEn { get; set; }
        public int StudentCount { get; set; }
    }
    public class DepartmentStudentCountProcParameters
    {
        public int DID { get; set; } = 0;
    }
}

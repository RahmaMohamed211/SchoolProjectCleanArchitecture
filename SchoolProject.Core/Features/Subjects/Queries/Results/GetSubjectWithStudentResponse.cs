using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Subjects.Queries.Results
{
    public class GetSubjectWithStudentResponse
    {
        public string SubjectNameAr { get; set; }
        public string SubjectNameEn { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string Address { get; set; }
        public string phone { get; set; }
        public string departmentName { get; set; }
        public int Period { get; set; }
        public decimal grade { get; set; }
    }
}

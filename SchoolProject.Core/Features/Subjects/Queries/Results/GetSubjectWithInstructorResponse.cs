using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Subjects.Queries.Results
{
    public class GetSubjectWithInstructorResponse
    {
        public string SubjectNameAr { get; set; }
        public string SubjectNameEn { get; set; }
        public string InsNameAr { get; set; }
        public string InsNameEn { get; set; }
        public string Address { get; set; }
        public string position { get; set; }
        public string salary { get; set; }
        public string departmentName { get; set; }
    }
}

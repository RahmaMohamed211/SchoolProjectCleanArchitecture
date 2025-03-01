using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Subjects.Queries.Results
{
    public class GetSubjectListResponse
    {
        public int Id { get; set; }
        public string? SubjectName { get; set; }
        
        public int? Period { get; set; }
        public string? DepartmentName { get; set; }
    }
}

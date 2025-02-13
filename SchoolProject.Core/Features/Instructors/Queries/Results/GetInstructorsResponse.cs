using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Instructors.Queries.Results
{
    public class GetInstructorsResponse
    {
        public int InsId { get; set; }
        public string? EName { get; set; }
   
        public string? Address { get; set; }
        public string? Position { get; set; }
        public decimal? Salary { get; set; }
        public string? Image { get; set; }
        public string? DapartmertName { get; set; }
    }
}

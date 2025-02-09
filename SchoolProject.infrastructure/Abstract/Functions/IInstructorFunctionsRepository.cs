using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.infrastructure.Abstract.Functions
{
    public interface IInstructorFunctionsRepository
    {
        public Task< decimal> GetSalarySummationOfInstructor(string query);
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Instructors.Queries.Models;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
   
    [ApiController]
    public class InstructorController : AppControllerBase
    {
        [HttpGet(Router.InstructorRoute.GetSalarySummationOfInstructor)]
        public async Task<IActionResult> GetSalarySummation()
        {

            return NewResult(await Mediator.Send(new GetSummationSalaryOfInstructorQuery()));

        }
    }
}

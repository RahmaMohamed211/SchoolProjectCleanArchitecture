using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Instructors.Commands.Models;
using SchoolProject.Core.Features.Instructors.Queries.Models;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
   
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class InstructorController : AppControllerBase
    {
        [HttpGet(Router.InstructorRoute.GetSalarySummationOfInstructor)]
        public async Task<IActionResult> GetSalarySummation()
        {

            return NewResult(await Mediator.Send(new GetSummationSalaryOfInstructorQuery()));

        }
        [HttpPost(Router.InstructorRoute.AddInstructor)]
        public async Task<IActionResult> AddInstructor([FromForm] AddInstructorCommand command)
        {

            return NewResult(await Mediator.Send(command));

        }
        [HttpGet(Router.InstructorRoute.List)]
       
        public async Task<IActionResult> GetInstructorList()
        {
            var response = await Mediator.Send(new GetInstructorsQuery());
            return Ok(response);

        }
        [HttpGet(Router.InstructorRoute.GetByID)]
        public async Task<IActionResult> GetInstructorByID([FromRoute] int id)
        {

            return NewResult(await Mediator.Send(new GetInstructorByIdQuery(id)));

        }
    }
}

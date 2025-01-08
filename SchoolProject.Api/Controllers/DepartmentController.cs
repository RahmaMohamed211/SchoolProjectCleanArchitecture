using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Departments.Commands.Models;
using SchoolProject.Core.Features.Departments.Queries.Models;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
    [ApiController]
    public class DepartmentController : AppControllerBase
    {
        [HttpGet(Router.DepartmentRouting.GetByID)]
        public async Task<IActionResult> GetDepartmentByID([FromQuery]GetDepartmentByIDQuery Query)
        {

            return NewResult(await Mediator.Send(Query));

        }
        [HttpPost(Router.DepartmentRouting.Create)]
        public async Task<IActionResult> Create([FromBody] AddDepartmentCommand command)
        {

            return NewResult(await Mediator.Send(command));

        }
        [HttpPut(Router.DepartmentRouting.Edit)]
        public async Task<IActionResult> Edit([FromBody] EditDepartmentCommand command)
        {

            return NewResult(await Mediator.Send(command));

        }
        [HttpDelete(Router.DepartmentRouting.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {

            return NewResult(await Mediator.Send(new DeleteDepartmentCommand(id)));

        }
    }
}

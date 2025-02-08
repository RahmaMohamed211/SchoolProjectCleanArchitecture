using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Core.Filters;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
   
    [ApiController]
    [Authorize(Roles ="Admin,User")]
    [ServiceFilter(typeof(AuthFilter))]
    public class StudentController :AppControllerBase
    {

        
        [HttpGet(Router.StudentRouting.List)]
        [Authorize(Roles ="User")]
        public  async Task<IActionResult > GetStudentList()
        {
            var response = await Mediator.Send(new GetStudentListQuery());
            return Ok(response);

        }
        [AllowAnonymous]
        [HttpGet(Router.StudentRouting.Paginted)]
        public async Task<IActionResult> Paginted([FromQuery] GetStudentPaginatedListQuery query  )
        {
            var response = await Mediator.Send(query);
            return Ok(response);

        }


        [HttpGet(Router.StudentRouting.GetByID)]
        public async Task<IActionResult> GetStudentByID([FromRoute]int id)
         {
          
            return NewResult(await Mediator.Send(new GetStudentByIDQuery(id)));

        }
        [Authorize(Policy = "CreateStudent")]
        [HttpPost(Router.StudentRouting.Create)]
        public async Task<IActionResult> Create([FromBody] AddStudentCommand command)
        {
          
            return NewResult(await Mediator.Send(command));

        }
        [Authorize(Policy = "EditStudent")]
        [HttpPut(Router.StudentRouting.Edit)]
        public async Task<IActionResult> Edit([FromBody] EditStudentCommand command)
        {

            return NewResult(await Mediator.Send(command));

        }
       // [Authorize(Policy = "DeleteStudent")]
        [HttpDelete(Router.StudentRouting.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {

            return NewResult(await Mediator.Send(new DeleteStudentCommand(id)));

        }


    }
}

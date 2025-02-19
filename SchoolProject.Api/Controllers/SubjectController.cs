using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Features.Subjects.Commands.Models;
using SchoolProject.Data.AppMetaData;
using Swashbuckle.AspNetCore.Annotations;

namespace SchoolProject.Api.Controllers
{
   
    [ApiController]
    public class SubjectController : AppControllerBase
    {
     
        [HttpPost(Router.SubjectRouting.Create)]
        public async Task<IActionResult> Create([FromBody] AddSubjectCommand command)
        {

            return NewResult(await Mediator.Send(command));

        }
        [HttpPost(Router.SubjectRouting.AddSubjectToStudent)]
        [SwaggerOperation(Summary = "اضافة مادة لاحد الطلاب", OperationId = "AddSubjectToStudent")]
        public async Task<IActionResult> AddSubjectToStudent([FromBody] AddSubjectToStudentCommand command)
        {

            return NewResult(await Mediator.Send(command));

        }
        [HttpPost(Router.SubjectRouting.AddSubjectToInstructor)]
        [SwaggerOperation(Summary = "اضافة مادة لاحد المدرسين", OperationId = "AddSubjectToInstructor")]
        public async Task<IActionResult> AddSubjectToInstructor([FromBody] AddSubjectToInstructorCommand command)
        {

            return NewResult(await Mediator.Send(command));

        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Core.Features.Subjects.Commands.Models;
using SchoolProject.Core.Features.Subjects.Queries.Models;
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
        [HttpDelete(Router.SubjectRouting.DeleteSubject)]
        [SwaggerOperation(Summary = "حذف مادة ", OperationId = "DeleteSubject")]
        public async Task<IActionResult> DeleteSubject([FromRoute] int id)
        {

            return NewResult(await Mediator.Send(new deleteSubjectCommand(id)));

        }
        [HttpDelete(Router.SubjectRouting.DeleteSubjectToStudent)]
        [SwaggerOperation(Summary = "حذف مادة لاحد الطلاب ", OperationId = "DeleteSubjectToStudent")]
        public async Task<IActionResult> DeleteSubjectToStudent([FromBody] deleteSubjectForStudentCommand command)
        {

            return NewResult(await Mediator.Send(command));

        }
        [HttpDelete(Router.SubjectRouting.DeleteSubjectToInstructor)]
        [SwaggerOperation(Summary = "حذف مادة لاحد الانستراكتور ", OperationId = "DeleteSubjectToInstructor")]
        public async Task<IActionResult> DeleteSubjectToInstructor([FromBody] deleteSubjectToInstructorCommand command)
        {

            return NewResult(await Mediator.Send(command));

        }
       
        [HttpPut(Router.SubjectRouting.EditSubject)]
       
        public async Task<IActionResult> EditSubject([FromBody] EditSubjectCommand command)
        {

            return NewResult(await Mediator.Send(command));

        }
        [HttpGet(Router.SubjectRouting.List)]
      
        public async Task<IActionResult> GetSubjectList()
        {
            var response = await Mediator.Send(new GetSubjectListQuery());
            return Ok(response);

        }
        [HttpGet(Router.SubjectRouting.GetByID)]
        public async Task<IActionResult> GetSubjectByID([FromRoute] int id)
        {

            return NewResult(await Mediator.Send(new GetSubjectByIdQuery(id)));

        }
        [HttpGet(Router.SubjectRouting.GetSubjectWithInstructor)]
        public async Task<IActionResult> GetSubjectWithInstructor([FromRoute] int id)
        {

            return NewResult(await Mediator.Send(new GetSubjectWithInstructorQuey(id)));

        }
        [HttpGet(Router.SubjectRouting.GetSubjectWithStudent)]
        public async Task<IActionResult> GetSubjectWithStudent([FromRoute] int id)
        {

            return NewResult(await Mediator.Send(new GetSubjectWithStudentQuery(id)));

        }
    }
}

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Students.Commands.Handlers
{
    public class StudentCommandHandler : ResponseHandler
                                       , IRequestHandler<AddStudentCommand, Response<string>>,
                                         IRequestHandler<EditStudentCommand, Response<string>>
                                       , IRequestHandler<DeleteStudentCommand, Response<string>>
    {

        #region fields
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion
        #region Constructors

        public StudentCommandHandler(IStudentService studentService
            ,IMapper mapper, IStringLocalizer<SharedResources> localizer) :base(localizer)
        {
            _studentService = studentService;
            _mapper = mapper;
            _localizer = localizer;
        }
        #endregion

        #region HandleFunction
        public async Task<Response<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            //mapping between request and student
            var studentmapper = _mapper.Map<Student>(request);
            //add
            var result= await _studentService.AddAsync(studentmapper);

            //retuen response
             if (result == "success") return Created("");
            else return  BadRequest<string>();

        }

        public async Task<Response<string>> Handle(EditStudentCommand request, CancellationToken cancellationToken)
        {

            //check if the id is exist or not
            var student= await _studentService.GetByIDAsync(request.Id);
            //return not found
            if(student == null) return NotFound<string>("Student is not found");
            //map محتاجين نعمل 
            //mapping between request and student
            var studentmapper = _mapper.Map(request,student);
            //call service that make edit
            var result = await _studentService.EditAsync(studentmapper);

            //return response
            if (result == "success") return Success($"Edit Sussessfully {studentmapper.StudID}");
            else  return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            //check if the id is exist or not
            var student = await _studentService.GetByIDAsync(request.Id);
            //return not found
            if (student == null) return NotFound<string>("Student is not found");
            //delete
            //call service that make delete
            var result = await _studentService.DeleteAsync(student);

            //return response
            if (result == "success") return Deleted<string>($"delete Sussessfully {request.Id}");
            else return BadRequest<string>();
        }
        #endregion,

    }
}

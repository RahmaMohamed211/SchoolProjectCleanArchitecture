using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Features.Subjects.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;
using SchoolProject.Service.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Subjects.Commands.Handlers
{
    public class SubjectCommandHandler : ResponseHandler
                                       , IRequestHandler<AddSubjectCommand, Response<string>>
                                       , IRequestHandler<AddSubjectToStudentCommand, Response<string>>
                                       , IRequestHandler<AddSubjectToInstructorCommand, Response<string>>
    {
        private readonly ISubjectService _subjectService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        #region fields

        #endregion
        #region ctor
        public SubjectCommandHandler(ISubjectService subjectService,IMapper mapper,IStringLocalizer<SharedResources> stringLocalizer):base(stringLocalizer)
        {
            _subjectService = subjectService;
            _mapper = mapper;
           _stringLocalizer = stringLocalizer;
        }

        
        #endregion
        #region function
        public async Task<Response<string>> Handle(AddSubjectCommand request, CancellationToken cancellationToken)
        {
            //mapping between request and student
            var subjectmapper = _mapper.Map<Subject>(request);
            //add
            var result = await _subjectService.AddSubject(subjectmapper);

            //retuen response
            if (result == "Success") return Created("");
            else return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(AddSubjectToStudentCommand request, CancellationToken cancellationToken)
        {
            var Studentsubjectmapper = _mapper.Map<StudentSubject>(request);
            var result= await _subjectService.AddsubjectToStudent(Studentsubjectmapper);
            switch (result) 
            {
                case "StudentNotFound": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.NotFound]);
                case "SubjectNotFound": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.NotFound]);
                case "AlreadyExsists": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.IsExist]);
            }
            return Success("");
        }

        public async Task<Response<string>> Handle(AddSubjectToInstructorCommand request, CancellationToken cancellationToken)
        {
            var result = await _subjectService.AddsubjectToInstructor(request.InsId,request.SubId);
            switch (result)
            {
                case "InstructorNotFound": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.NotFound]);
                case "SubjectNotFound": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.NotFound]);
                case "AlreadyExsists": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.IsExist]);
            }
            return Success("");
        }
        #endregion
    }
}

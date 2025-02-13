using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Instructors.Commands.Models;
using SchoolProject.Core.Features.Instructors.Queries.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;
using SchoolProject.Service.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Instructors.Commands.Handlers
{
    public class InstructorCommandHandler : ResponseHandler,
                                  IRequestHandler<AddInstructorCommand, Response<string>>,
                                  IRequestHandler<EditInstructorCommand, Response<string>>,
                                  IRequestHandler<DeleteInstructorCommand, Response<string>>

    {

        #region fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly IInstructorService _instructorService;
        #endregion
        #region ctor
        public InstructorCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,IMapper mapper,IInstructorService instructorService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _instructorService = instructorService;
        }



        #endregion
        #region functions
        public async Task<Response<string>> Handle(AddInstructorCommand request, CancellationToken cancellationToken)
        {
            var instructor = _mapper.Map<Instructor>(request);
            var result = await _instructorService.AddInstrucorAsync(instructor, request.Image);
            switch (result)
            {
                case "NoImage": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.NoImage]);
                case "FailedToUploadImage": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUploadImage]);
                case "FailedInAdd": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.AddFailed]);
            
         
               
            }
                return  Success("");
            }

        public async Task<Response<string>> Handle(EditInstructorCommand request, CancellationToken cancellationToken)
        {
            //check if the id is exist or not
            var instructorExist = await _instructorService.GetByIDAsync(request.id);
            //return not found
        if (instructorExist == null)  return NotFound<string>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            //map محتاجين نعمل 
            //mapping between request and instructor
            var instructor=_mapper.Map(request, instructorExist);
            //call service that make edit
            var result= await _instructorService.EditInstructorAsync(instructor,request.Image);
            switch (result)
            {
                case "NoImage": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.NoImage]);
                case "FailedToUploadImage": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUploadImage]);
                case "FailedInEdit": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.EditFailed]);



            }
            return Success("");
        }

        public async Task<Response<string>> Handle(DeleteInstructorCommand request, CancellationToken cancellationToken)
        {
            //check if the id is exist or not
            var instructorExist = await _instructorService.GetByIDAsync(request.Id);
            //return not found
            if (instructorExist == null) return NotFound<string>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            //call service that make delete
            var result = await _instructorService.DeleteAsync(instructorExist);

            //return response
            if (result == "success") return Deleted<string>($"delete Sussessfully {request.Id}");
            else return BadRequest<string>();
        }
        #endregion

    }
}

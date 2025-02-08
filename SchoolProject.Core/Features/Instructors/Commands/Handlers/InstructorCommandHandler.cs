using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Instructors.Commands.Models;
using SchoolProject.Core.Features.Instructors.Queries.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Instructors.Commands.Handlers
{
    public class InstructorCommandHandler : ResponseHandler,
                                  IRequestHandler<AddInstructorCommand, Response<string>>

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
        #endregion

    }
}

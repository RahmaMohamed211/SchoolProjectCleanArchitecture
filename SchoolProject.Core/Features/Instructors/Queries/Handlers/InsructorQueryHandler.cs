using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Instructors.Queries.Models;
using SchoolProject.Core.Features.Instructors.Queries.Results;
using SchoolProject.Core.Features.Students.Queries.Results;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Instructors.Queries.Handlers
{
    public class InsructorQueryHandler:ResponseHandler,
                                  IRequestHandler<GetSummationSalaryOfInstructorQuery,Response<decimal>>,
                                  IRequestHandler<GetInstructorsQuery, Response<List<GetInstructorsResponse>>>,
                                  IRequestHandler<GetInstructorByIdQuery, Response<GetInstructorsResponse>>
    {

        #region fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IInstructorService _instructorService;
        private readonly IMapper _mapper;
        #endregion
        #region ctor
        public InsructorQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,IInstructorService instructorService,IMapper mapper):base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _instructorService = instructorService;
            _mapper = mapper;
        }


        #endregion
        #region functions
        public async Task<Response<decimal>> Handle(GetSummationSalaryOfInstructorQuery request, CancellationToken cancellationToken)
        {
           var result= await _instructorService.GetSalarySummationOfInstructor();
            return Success( result);
        }

        public async Task<Response<List<GetInstructorsResponse>>> Handle(GetInstructorsQuery request, CancellationToken cancellationToken)
        {
            var InsructorResult = await _instructorService.GetInstructorListAsync();
            if (InsructorResult == null)
                return NotFound<List<GetInstructorsResponse>>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            var InstructorMapper=_mapper.Map<List<GetInstructorsResponse>>(InsructorResult);
     
           return Success(InstructorMapper);
        }

        public async Task<Response<GetInstructorsResponse>> Handle(GetInstructorByIdQuery request, CancellationToken cancellationToken)
        {
            var instructor = await _instructorService.GetInstructorByIdAsyncwithInclude(request.id);
            if (instructor == null)
                return NotFound<GetInstructorsResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            var instructorMapper= _mapper.Map<GetInstructorsResponse>(instructor);
            return Success(instructorMapper);
        }
        #endregion
    }
}

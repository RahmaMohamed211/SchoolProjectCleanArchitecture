using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Core.Features.Students.Queries.Results;
using SchoolProject.Core.Features.Subjects.Queries.Models;
using SchoolProject.Core.Features.Subjects.Queries.Results;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;
using SchoolProject.Service.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Subjects.Queries.Handlers
{
    public class SubjectQueryHandler : ResponseHandler
        , IRequestHandler<GetSubjectListQuery, Response<List<GetSubjectListResponse>>>
        , IRequestHandler<GetSubjectByIdQuery, Response<GetSubjecByIdResponse>>
        , IRequestHandler<GetSubjectWithInstructorQuey, Response<GetSubjectWithInstructorResponse>>
        , IRequestHandler<GetSubjectWithStudentQuery, Response<GetSubjectWithStudentResponse>>
    {

        #region fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly ISubjectService _subjectService;
        #endregion
        #region ctor
        public SubjectQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,IMapper mapper,ISubjectService subjectService):base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _subjectService = subjectService;
        }


        #endregion
        #region functions
        public async Task<Response<List<GetSubjectListResponse>>> Handle(GetSubjectListQuery request, CancellationToken cancellationToken)
        {
            var SubjectList = await _subjectService.GetSubjectsListAsync();
            var SubjectListMapper = _mapper.Map<List<GetSubjectListResponse>>(SubjectList);
            var result = Success(SubjectListMapper);
          
            return result;
        }

        public async Task<Response<GetSubjecByIdResponse>> Handle(GetSubjectByIdQuery request, CancellationToken cancellationToken)
        {
            var subject = await _subjectService.GetSubjectByIDAsyncWithInclude(request.Id);
            if (subject == null) return NotFound<GetSubjecByIdResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            var result = _mapper.Map<GetSubjecByIdResponse>(subject);
            return Success(result);
        }

        public async Task<Response<GetSubjectWithInstructorResponse>> Handle(GetSubjectWithInstructorQuey request, CancellationToken cancellationToken)
        {
            var subject = await _subjectService.GetSubjectByInstructorAsync(request.ID);
            if (subject == null) return NotFound<GetSubjectWithInstructorResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            var result = _mapper.Map<GetSubjectWithInstructorResponse>(subject);
            return Success(result);
        }

        public async Task<Response<GetSubjectWithStudentResponse>> Handle(GetSubjectWithStudentQuery request, CancellationToken cancellationToken)
        {
            var subject = await _subjectService.GetSubjectByStudnentAsync(request.ID);
            if (subject == null) return NotFound<GetSubjectWithStudentResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            var result = _mapper.Map<GetSubjectWithStudentResponse>(subject);
            return Success(result);
        }
        #endregion
    }
}

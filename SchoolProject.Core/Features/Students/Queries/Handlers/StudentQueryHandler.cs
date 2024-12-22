using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Core.Features.Students.Queries.Results;
using SchoolProject.Core.Resources;
using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Students.Queries.Handlers
{
    public class StudentQueryHandler :ResponseHandler
        , IRequestHandler<GetStudentListQuery,Response<List<GetStudentListResponse>>>,
         IRequestHandler<GetStudentByIDQuery, Response<GetSingleStudentResponse>>,
         IRequestHandler<GetStudentPaginatedListQuery, PaginatedResult<GetStudentPaginatedListResonse>>
    {

        #region Fields
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        #endregion
        #region constructors
        public StudentQueryHandler(IStudentService studentService,IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer):base(stringLocalizer)
        {
            _studentService = studentService;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }

        #endregion
        #region HandleFunction

       
        public async Task<Response< List<GetStudentListResponse>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
            var StudentList= await _studentService.GetStudentsListAsync();
            var StudentListMapper = _mapper.Map<List<GetStudentListResponse>>(StudentList);
            var result= Success( StudentListMapper);
            result.Meta = new {count= StudentListMapper.Count()};//دة anaymous هيعرفه وقت run time
            return result;
        }

        public async Task<Response<GetSingleStudentResponse>> Handle(GetStudentByIDQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetStudentByIDwithIncludeAsync(request.Id);
            if (student == null)  return NotFound<GetSingleStudentResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            var result= _mapper.Map<GetSingleStudentResponse>(student);
            return Success( result );  
        }

        public async Task<PaginatedResult<GetStudentPaginatedListResonse>> Handle(GetStudentPaginatedListQuery request, CancellationToken cancellationToken)
          {
            //بيحتاج يتحول عشان داتا بيز يفهمه
            Expression<Func<Student, GetStudentPaginatedListResonse>> expression = e => new GetStudentPaginatedListResonse(e.StudID,e.Localize(e.NameAr,e.NameEn),e.Address,e.Department.Localize(e.Department.DNameAr, e.Department.DNameEn));

            var filterQuery = _studentService.FilterStudentPaginatedQuerable(request.OrderBy,request.Search);
            var paginatedList = await filterQuery.Select(expression).ToPaginatedListAsync(request.PageNumer, request.PageSize);
            paginatedList.Meta=new {count=paginatedList.Data.Count()};
            return paginatedList;

        }
        #endregion
    }
}

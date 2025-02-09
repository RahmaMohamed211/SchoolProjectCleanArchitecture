using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Departments.Queries.Models;
using SchoolProject.Core.Features.Departments.Queries.Results;
using SchoolProject.Core.Features.Students.Queries.Results;
using SchoolProject.Core.Resources;
using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Procedures;
using SchoolProject.Service.Abstracts;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Departments.Queries.Handlers
{
    public class DepartmentQueryHandler:ResponseHandler,
        IRequestHandler<GetDepartmentByIDQuery,Response<GetDepartmentByIDResponse>>,
        IRequestHandler<GetDepartmentStudentListCountQuery, Response<List<GetDepartmentStudentListCountResults>>>,
        IRequestHandler<GetDepartmentStudentCountByIDQuery, Response<GetDepartmentStudentCountByIDResult>>
    {


        #region Fields
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IStudentService _studentService;
        #endregion
        #region Constructor
        public DepartmentQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,IStudentService studentService,IDepartmentService departmentService,IMapper mapper):base(stringLocalizer) 
        {
            _stringLocalizer = stringLocalizer;
            _studentService = studentService;
            _departmentService = departmentService;
            _mapper = mapper;
        }


        #endregion
        #region handle function
        public async Task<Response<GetDepartmentByIDResponse>> Handle(GetDepartmentByIDQuery request, CancellationToken cancellationToken)
        {

            //service get by id include st sub ins
            var response = await _departmentService.GetDepartmentByID(request.Id);
            //check is not exist //not found
            if (response == null)  return NotFound<GetDepartmentByIDResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            //mapping يجيب داتا
            var mapper=_mapper.Map<GetDepartmentByIDResponse>(response);

            //pagination
            Expression<Func<Student, StudentResponse>> expression = e => new StudentResponse(e.StudID, e.Localize(e.NameAr, e.NameEn));
            var studentQuerable = _studentService.GetStudentsByDepartmentIDQuerable(request.Id);
            var paginatedList = await studentQuerable.Select(expression).ToPaginatedListAsync(request.StudentPageNumber, request.StudentPageSize);
            mapper.StudentList= paginatedList;
            Log.Information($"Get Department BY Id {request.Id} !");  
            //return response
            return Success(mapper);
        }

        public async Task<Response<List<GetDepartmentStudentListCountResults>>> Handle(GetDepartmentStudentListCountQuery request, CancellationToken cancellationToken)
        {
            var ViewDepartmentresult = await _departmentService.GetViewDepartmentDataAsync();
            var result = _mapper.Map<List<GetDepartmentStudentListCountResults>>(ViewDepartmentresult);
            return Success(result);
        }

        public async Task<Response<GetDepartmentStudentCountByIDResult>> Handle(GetDepartmentStudentCountByIDQuery request, CancellationToken cancellationToken)
        {
            var parameters = _mapper.Map<DepartmentStudentCountProcParameters>(request);
            var procresult= await _departmentService.GetDepartmentStudentCountProcAsync(parameters);
            var result = _mapper.Map<GetDepartmentStudentCountByIDResult>(procresult.FirstOrDefault());
            return Success(result);
        }

        #endregion
    }
}

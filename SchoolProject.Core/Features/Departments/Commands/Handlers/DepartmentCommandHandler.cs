using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Departments.Commands.Models;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Features.Students.Queries.Results;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;
using SchoolProject.Service.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Departments.Commands.Handlers
{
    public class DepartmentCommandHandler : ResponseHandler
                                       , IRequestHandler<AddDepartmentCommand, Response<string>>
                                       ,IRequestHandler<EditDepartmentCommand, Response<string>>
                                       ,IRequestHandler<DeleteDepartmentCommand, Response<string>>

    {

        #region fields
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion
        #region Constructors

        public DepartmentCommandHandler(IDepartmentService departmentService
            , IMapper mapper, IStringLocalizer<SharedResources> localizer) : base(localizer)
        {
            _departmentService = departmentService;
            _mapper = mapper;
            _localizer = localizer;
        }
        #endregion

        #region HandleFunction
   
        public async Task<Response<string>> Handle(AddDepartmentCommand request, CancellationToken cancellationToken)
        {
            //mapping betwen request and department
            var DepartmentMapper = _mapper.Map<Department>(request);
            //add
            var result = await _departmentService.AddDepartmentAsync(DepartmentMapper);
            //retuen response
            if (result == "success") 
                return Created("");
            else
                return BadRequest<string>();

        }

        public async Task<Response<string>> Handle(EditDepartmentCommand request, CancellationToken cancellationToken)
        {
            //check if the id is exist or not
            var department = await _departmentService.GetDepartmentByID(request.Id);
            //return not found
            if (department == null) return NotFound<string>("Department is not found");
            //map محتاجين نعمل 
            //mapping between request and student
            var Departmentmapper = _mapper.Map(request, department);
            //call service that make edit
            var result = await _departmentService.EditAsync(Departmentmapper);

            //return response
            if (result == "success") return Success($"Edit Sussessfully {Departmentmapper.DId}");
            else return BadRequest<string>();


        }

        public async Task<Response<string>> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            //check if the id is exist or not
            var department = await _departmentService.GetDepartmentByID(request.Id);
            //return not found
            if (department == null) 
                return  NotFound<string>(_localizer[SharedResourcesKeys.NotFound]);
            //delete
            //call service that make delete
            var result = await _departmentService.DeleteAsync(department);

            //return response
            if (result == "success") return Deleted<string>($"delete Sussessfully {request.Id}");
            else return BadRequest<string>();
        }



        #endregion
    }
}



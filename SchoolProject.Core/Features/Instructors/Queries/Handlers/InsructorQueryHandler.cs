using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Instructors.Queries.Models;
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
                                  IRequestHandler<GetSummationSalaryOfInstructorQuery,Response<decimal>>
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
        #endregion
    }
}

using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Departments.Commands.Models;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Departments.Commands.Validation
{
    public class AddDepartmentValidation: AbstractValidator<AddDepartmentCommand>
    {

        #region fields
        private readonly IDepartmentService _departmentService;
        private readonly IStringLocalizer<SharedResources> _localizer;
  
        #endregion
        #region Ctor
        public AddDepartmentValidation(IDepartmentService departmentService
                                 , IStringLocalizer<SharedResources> localizer)
        {
           
            _localizer = localizer;
            _departmentService = departmentService;
            ApplyValidationRules();
            ApplyCustomValidationRules();


        }
        #endregion
        #region Actions
        public void ApplyValidationRules()
        {
            RuleFor(x => x.DNameAr)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                .MaximumLength(100).WithMessage(_localizer[SharedResourcesKeys.MaxLenghtis100]);

            RuleFor(x => x.DNameEn)
               .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
               .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
               .MaximumLength(100).WithMessage(_localizer[SharedResourcesKeys.MaxLenghtis100]);


        }

        public void ApplyCustomValidationRules()
        {
            RuleFor(x => x.DNameEn)
                .MustAsync(async (Key, CancellationToken) => !await _departmentService.IsNameEnExist(Key))
            .WithMessage(_localizer[SharedResourcesKeys.IsExist]);

            RuleFor(x => x.DNameAr)
              .MustAsync(async (Key, CancellationToken) => !await _departmentService.IsNameArExist(Key))
          .WithMessage(_localizer[SharedResourcesKeys.IsExist]);






        }

        #endregion


    }
}


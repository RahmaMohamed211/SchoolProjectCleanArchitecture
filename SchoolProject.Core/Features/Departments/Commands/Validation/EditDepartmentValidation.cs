using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Departments.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;
using SchoolProject.Service.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Departments.Commands.Validation
{
    public class EditDepartmentValidation:AbstractValidator<EditDepartmentCommand>
    {
        private readonly IDepartmentService _departmentService;
        private readonly IStringLocalizer<SharedResources> _localizer;
        #region fields

        #endregion
        #region ctor
        public EditDepartmentValidation(IDepartmentService departmentService, IStringLocalizer<SharedResources> localizer)
        {
            _departmentService = departmentService;
            _localizer = localizer;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }

        #endregion
        #region function
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
                  .MustAsync(async (model, Key, CancellationToken) => !await _departmentService.IsNameExistEnExcludeSelf(Key, model.Id))
              .WithMessage("DepartmentNameEn is Exist");
            RuleFor(x => x.DNameAr)
                .MustAsync(async (model, Key, CancellationToken) => !await _departmentService.IsNameExistArExcludeSelf(Key, model.Id))
            .WithMessage("DepartmentNameAr is Exist");




        }

        #endregion
    }
}

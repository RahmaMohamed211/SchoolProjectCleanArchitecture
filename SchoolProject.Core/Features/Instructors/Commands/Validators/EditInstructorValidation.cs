using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Instructors.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Instructors.Commands.Validators
{
    public class EditInstructorValidation : AbstractValidator<AddInstructorCommand>
    {

        #region fields

        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IInstructorService _instructorService;
        private readonly IDepartmentService _departmentService;
        #endregion
        #region Ctor
        public EditInstructorValidation(IStringLocalizer<SharedResources> localizer, IInstructorService instructorService, IDepartmentService departmentService)
        {

            _localizer = localizer;
            _instructorService = instructorService;
            _departmentService = departmentService;
            ApplyValidationRules();
            ApplyCustomValidationRules();


        }
        #endregion
        #region Actions
        public void ApplyValidationRules()
        {
            RuleFor(x => x.NameAr)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                .MaximumLength(10).WithMessage(_localizer[SharedResourcesKeys.MaxLenghtis100]);

            RuleFor(x => x.NameEn)
              .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
              .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
              .MaximumLength(10).WithMessage(_localizer[SharedResourcesKeys.MaxLenghtis100]);


            RuleFor(x => x.Address)
              .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
              .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
              .MaximumLength(20).WithMessage(_localizer[SharedResourcesKeys.MaxLenghtis100]);

            RuleFor(x => x.DID)
             .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
             .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);


        }

        public void ApplyCustomValidationRules()
        {
            RuleFor(x => x.NameAr)
                .MustAsync(async (Key, CancellationToken) => !await _instructorService.IsNameArExist(Key))
            .WithMessage(_localizer[SharedResourcesKeys.IsExist]);

            RuleFor(x => x.NameEn)
              .MustAsync(async (Key, CancellationToken) => !await _instructorService.IsNameEnExist(Key))
          .WithMessage(_localizer[SharedResourcesKeys.IsExist]);


            //RuleFor(x => x.DID)
            //  .MustAsync(async (Key, CancellationToken) => await _departmentService.IsDepartmentIdExist(Key))
            //  .WithMessage(_localizer[SharedResourcesKeys.IsNoExist]);




        }

        #endregion

    }
}

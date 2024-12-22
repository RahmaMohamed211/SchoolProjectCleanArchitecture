using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Students.Commands.Validation
{
    public class AddStudentValidator :AbstractValidator<AddStudentCommand>
    {

        #region fields
        private readonly IStudentService _studentService;
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion
        #region Ctor
        public AddStudentValidator(IStudentService studentService
                                 , IStringLocalizer<SharedResources> localizer)
        {
            _studentService = studentService;
            _localizer = localizer;
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

            RuleFor(x => x.Address)
              .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
              .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
              .MaximumLength(20).WithMessage(_localizer[SharedResourcesKeys.MaxLenghtis100]);

        }

        public void ApplyCustomValidationRules()
        {
            RuleFor(x => x.NameAr)
                .MustAsync(async (Key, CancellationToken) => !await _studentService.IsNameExist(Key))
            .WithMessage("Name is Exist");

            RuleFor(x => x.NameEn)
              .MustAsync(async (Key, CancellationToken) => !await _studentService.IsNameExist(Key))
          .WithMessage("Name is Exist");

        }

            #endregion


        }
}

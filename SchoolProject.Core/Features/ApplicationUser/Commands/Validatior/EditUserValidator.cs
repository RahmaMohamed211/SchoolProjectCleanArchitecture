using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Validatior
{
    public class EditUserValidator: AbstractValidator<EditUserCommand>
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        #region fields

        #endregion
        #region ctor
        public EditUserValidator(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            ApplyValidationRules();
            ApplyCustomValidationRules();

        }
        #endregion
        #region function
        public void ApplyValidationRules()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                .MaximumLength(100).WithMessage(_localizer[SharedResourcesKeys.MaxLenghtis100]);

            RuleFor(x => x.UserName)
               .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])

               .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
               .MaximumLength(100).WithMessage(_localizer[SharedResourcesKeys.MaxLenghtis100]);


            RuleFor(x => x.Email)
               .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
               .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);

          


        }

        public void ApplyCustomValidationRules()
        {



        }

        #endregion
    }
}


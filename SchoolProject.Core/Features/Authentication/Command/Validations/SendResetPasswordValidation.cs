using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authentication.Command.Models;
using SchoolProject.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Authentication.Command.Validations
{
    public class SendResetPasswordValidation : AbstractValidator<SendResetPasswordCommand>
    {

        #region fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion
        #region ctor
        public SendResetPasswordValidation(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            ApplyValidationRules();
            ApplyCustomValidationRules();

        }
        #endregion
        #region function
        public void ApplyValidationRules()
        {

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

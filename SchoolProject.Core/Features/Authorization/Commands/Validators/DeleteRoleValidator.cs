using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Authorization.Commands.Validators
{
    public class DeleteRoleValidator:AbstractValidator<DeleteRoleCommand>
    {
        private readonly IAuthorizationService _authorizationService;
        #region fields
        private readonly IStringLocalizer<SharedResources> _localizer;
      
        #endregion
        #region ctor
        public DeleteRoleValidator(IAuthorizationService authorizationService, IStringLocalizer<SharedResources> localizer)
        {
            _authorizationService = authorizationService;
            _localizer = localizer;
            ApplyValidationRules();
            ApplyCustomValidationRules();

        }
        #endregion
        #region function
        public void ApplyValidationRules()
        {

            RuleFor(x => x.Id)
               .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
               .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);

        }

        public void ApplyCustomValidationRules()
        {
            //RuleFor(x => x.Id)
            //   .MustAsync(async (Key, CancellationToken) => await _authorizationService.IsRoleExistById(Key))
            //  .WithMessage(_localizer[SharedResourcesKeys.RoleNotExist]);




        }

        #endregion
    }
}

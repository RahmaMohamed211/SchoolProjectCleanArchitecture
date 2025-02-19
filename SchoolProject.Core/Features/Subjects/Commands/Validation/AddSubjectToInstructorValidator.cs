using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Subjects.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Subjects.Commands.Validation
{
    public class AddSubjectToInstructorValidator:AbstractValidator<AddSubjectToInstructorCommand> 
    {


        #region fields 
        
        private readonly IStringLocalizer<SharedResources> _localizer;

        #endregion
        #region Ctor
        public AddSubjectToInstructorValidator(
                                  IStringLocalizer<SharedResources> localizer)
        {
            
            _localizer = localizer;
            ApplyValidationRules();
          


        }
        #endregion
        #region Actions
        public void ApplyValidationRules()
        {
            RuleFor(x => x.SubId)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);


            RuleFor(x => x.InsId)
              .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
              .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);


           


        }

      

        #endregion


    }


    
}

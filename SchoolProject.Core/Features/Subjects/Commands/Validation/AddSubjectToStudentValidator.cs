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
    public class AddSubjectToStudentValidator : AbstractValidator<AddSubjectToStudentCommand>
    {


        #region fields 
        private readonly ISubjectService _subjectService;
        private readonly IDepartmentService _departmentService;
        private readonly IStringLocalizer<SharedResources> _localizer;

        #endregion
        #region Ctor
        public AddSubjectToStudentValidator(ISubjectService subjectService, IDepartmentService departmentService
                                 , IStringLocalizer<SharedResources> localizer)
        {
            _subjectService = subjectService;
            _departmentService = departmentService;
            _localizer = localizer;
            ApplyValidationRules();
            ApplyCustomValidationRules();


        }
        #endregion
        #region Actions
        public void ApplyValidationRules()
        {
            RuleFor(x => x.SubId)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);


            RuleFor(x => x.StudentId)
              .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
              .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);
             

            RuleFor(x => x.grade)
              .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
              .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);



        }

        public void ApplyCustomValidationRules()
        {
           






        }

        #endregion


    }

}

﻿using FluentValidation;
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
    internal class EditSubjectValidator : AbstractValidator<EditSubjectCommand>
    {


        #region fields 
        private readonly ISubjectService _subjectService;
        private readonly IDepartmentService _departmentService;
        private readonly IStringLocalizer<SharedResources> _localizer;

        #endregion
        #region Ctor
        public EditSubjectValidator(ISubjectService subjectService, IDepartmentService departmentService
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
            RuleFor(x => x.SubjectNameAr)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                .MaximumLength(100).WithMessage(_localizer[SharedResourcesKeys.MaxLenghtis100]);

            RuleFor(x => x.SubjectNameEn)
              .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
              .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
              .MaximumLength(100).WithMessage(_localizer[SharedResourcesKeys.MaxLenghtis100]);

            RuleFor(x => x.Period)
              .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
              .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);



        }

        public void ApplyCustomValidationRules()
        {
            RuleFor(x => x.SubjectNameAr)
                .MustAsync(async (Key, CancellationToken) => !await _subjectService.IsNameArExist(Key))
            .WithMessage(_localizer[SharedResourcesKeys.IsExist]);

            RuleFor(x => x.SubjectNameEn)
              .MustAsync(async (Key, CancellationToken) => !await _subjectService.IsNameEnExist(Key))
          .WithMessage(_localizer[SharedResourcesKeys.IsExist]);








        }

        #endregion


    }
}


using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Emails.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Emails.Commands.Handlers
{
    public class EmailsCommandHandler:ResponseHandler
                                ,IRequestHandler<SendEmailCommand,Response<string>>
    {


        #region fields
        private readonly IEmailService _emailService;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        #endregion
        #region ctor
        public EmailsCommandHandler(IEmailService emailService,IStringLocalizer<SharedResources> stringLocalizer):base(stringLocalizer) 
        {
            _emailService = emailService;
            _stringLocalizer = stringLocalizer;
        }
        #endregion
        #region functions
        public async Task<Response<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
             var response =await _emailService.SendEmail(request.Email, request.Message,null);
            if (response== "Success") 
            {
                return Success<string>("");
            } 
            return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.SendEmailFailed]);
        }
        #endregion
    }
}

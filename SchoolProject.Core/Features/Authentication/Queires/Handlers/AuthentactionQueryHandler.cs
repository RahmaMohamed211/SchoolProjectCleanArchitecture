using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authentication.Command.Models;
using SchoolProject.Core.Features.Authentication.Queires.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Authentication.Queires.Handlers
{
    public class AuthentactionQueryHandler : ResponseHandler,
        IRequestHandler<AuthorizeUserQuery, Response<string>>
    {
        #region fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IAuthenicationService _authenicationService;

        #endregion
        #region ctor
        public AuthentactionQueryHandler(IAuthenicationService authenicationService, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _authenicationService = authenicationService;
            
            _stringLocalizer = stringLocalizer;
        }
        #endregion
        #region functions
        public async Task<Response<string>> Handle(AuthorizeUserQuery request, CancellationToken cancellationToken)
        {
            var result = await _authenicationService.ValidateToken(request.AccessToken);
            if (result == "NotExpired") 
                return Success(result);
            return BadRequest<string>("Expired");
        }
        #endregion

    

    }
}

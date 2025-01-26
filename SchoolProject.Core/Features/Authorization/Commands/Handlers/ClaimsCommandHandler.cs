using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Authorization.Commands.Handlers
{
    public class ClaimsCommandHandler : ResponseHandler,
        IRequestHandler<UpdateUserClaimsCommand, Response<string>>
    {



        #region fileds
        private readonly UserManager<User> userManager;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        #endregion
        #region ctor
        public ClaimsCommandHandler(UserManager<User> userManager,IAuthorizationService authorizationService,IStringLocalizer<SharedResources> stringLocalizer):base(stringLocalizer) 
        {
            this.userManager = userManager;
            _authorizationService = authorizationService;
            _stringLocalizer = stringLocalizer;
        }


        #endregion
        #region functions
        public async Task<Response<string>> Handle(UpdateUserClaimsCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.UpdateUserClaims(request);
            switch (result)
            {
                case "UserIsNull": return NotFound<string>(_stringLocalizer[SharedResourcesKeys.UserIsNotFound]);
                case "FailedToRemoveOldClaim": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToRemoveOldCliams]);
                case "FialedToAddNewClaims": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToAddNewCliams]);
                case "FailedToUpdateClaims": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUpdateNewCliams]);
            }
            return Success<string>(_stringLocalizer[SharedResourcesKeys.Success]);

        }
        #endregion
    }
}

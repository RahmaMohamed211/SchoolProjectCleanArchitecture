using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authentication.Command.Models;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Authorization.Commands.Handlers
{
    public class RoleCommandHandler : ResponseHandler,
        IRequestHandler<AddRoleCommand, Response<string>>,
        IRequestHandler<EditRoleCommand, Response<string>>,
        IRequestHandler<DeleteRoleCommand , Response<string>>,
        IRequestHandler<UpdateUserRolesCommand , Response<string>>
    {
  

        #region fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IAuthorizationService _authorizationService;
        #endregion
        #region ctor
        public RoleCommandHandler(IAuthorizationService authorizationService,IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
         
            _authorizationService = authorizationService;
            _stringLocalizer = stringLocalizer;
        }
        #endregion
        #region functions
        public async Task<Response<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.AddRoleAsync(request.RoleName);
            if (result== "success") return Success("");
            return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.AddFailed]);

        }
        public async Task<Response<string>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
           var result=await _authorizationService.EditRoleAsync(request);
            if (result == "NotFound") return NotFound<string>();
            else if(result == "success") return Success((string)_stringLocalizer[SharedResourcesKeys.Updated]);
            else
                return BadRequest<string>(result);

        }

        public async Task<Response<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.DeleteRoleAsync(request.Id);
            if (result == "NotFound") return NotFound<string>();
            else if (result == "Used") return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.RoleIsUsed]);
            else if (result == "Success") return Success((string)_stringLocalizer[SharedResourcesKeys.Deleted]);
            else
                return BadRequest<string>(result);


        }

        public async Task<Response<string>> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.UpdateUserRoles(request);
            switch (result) 
            {
                case "UserIsNull": return NotFound<string>(_stringLocalizer[SharedResourcesKeys.UserIsNotFound]);
                case "FailedToRemoveOldRoles": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToRemoveOldRoles]);
                case "FialedToAddNewRoles": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToAddNewRoles]);
                case "FailedToUpdateuserRoles": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUpdateNewRoles]);
            }
             return Success<string>(_stringLocalizer[SharedResourcesKeys.Success]);

            }
        #endregion

    }
}

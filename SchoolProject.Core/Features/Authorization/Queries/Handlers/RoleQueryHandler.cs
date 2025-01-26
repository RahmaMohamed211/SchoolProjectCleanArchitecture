using AutoMapper;
using MediatR;

using Microsoft.AspNetCore.Identity;

using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.Features.Authorization.Queries.Models;
using SchoolProject.Core.Features.Authorization.Queries.Results;
using SchoolProject.Core.Resources;

using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Results;


using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Authorization.Queries.Handlers
{
    public class RoleQueryHandler : ResponseHandler,
        IRequestHandler<GetRolesListQuery, Response<List<GetRolesListResult>>>,
        IRequestHandler<GetRoleByIdQuery, Response<GetRoleByIdResult>>,
        IRequestHandler<ManageUserRoleQuery, Response<ManageUserRoleResult>>
        

    {
       
        #region fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;


        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IAuthorizationService _authorizationService;
        #endregion
        #region ctor

       
        public RoleQueryHandler(UserManager<User> userManager, IMapper mapper,IAuthorizationService authorizationService,IStringLocalizer<SharedResources> stringLocalizer):base(stringLocalizer) 
        {
            _userManager = userManager;
            _mapper = mapper;
            _authorizationService = authorizationService;
            _stringLocalizer = stringLocalizer;
        }

        #endregion
        #region function
      public  async Task<Response<List<GetRolesListResult>>> Handle(GetRolesListQuery request, CancellationToken cancellationToken)
        {
            var roles = await _authorizationService.GetRolesList();
            var result= _mapper.Map<List<GetRolesListResult>>(roles);
            return Success(result);
        }

        public async Task<Response<GetRoleByIdResult>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role=await _authorizationService.GetRoleById(request.Id);
            if (role == null) return NotFound<GetRoleByIdResult>(_stringLocalizer[SharedResourcesKeys.RoleNotExist]);
            var result=_mapper.Map<GetRoleByIdResult>(role);
            return Success(result);
             
        }


        public async Task<Response<ManageUserRoleResult>> Handle(ManageUserRoleQuery request, CancellationToken cancellationToken)
        {
            //هنبعت list of roles //هنبدا نشوف roles بتاعت يوزر//make for loop هل role موجود ولا لا 
            var user = await _userManager.FindByIdAsync( request.UserId.ToString());
            if (user == null) return NotFound<ManageUserRoleResult>(_stringLocalizer[SharedResourcesKeys.UserIsNotFound]);
            var result =await _authorizationService.ManageUserRolesData(user);
            return Success(result);


        }

        #endregion

    }
}

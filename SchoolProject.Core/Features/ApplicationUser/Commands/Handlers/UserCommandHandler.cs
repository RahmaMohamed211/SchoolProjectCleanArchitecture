using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Handlers
{
    public class UserCommandHandler : ResponseHandler, IRequestHandler<AddUserCommand, Response<string>>
    {
        private readonly IStringLocalizer<SharedResources> _sharedLocalizer;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        #region fields

        #endregion
        #region ctor
        public UserCommandHandler(IStringLocalizer<SharedResources> sharedLocalizer, IMapper mapper, UserManager<User> userManager) : base(sharedLocalizer)
        {
            _sharedLocalizer = sharedLocalizer;
            _mapper = mapper;
            _userManager = userManager;
        }
        #endregion
        #region handelFuction
       
        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            //if email is exist
            var user = await _userManager.FindByEmailAsync(request.Email);
            //email is exist
            if (user != null) return BadRequest<string>(_sharedLocalizer[SharedResourcesKeys.EmailIsExist]);

            //if username is exist
           var userByUserName= await _userManager.FindByNameAsync(request.UserName);
            //username is exist
            if (userByUserName != null) return BadRequest<string>(_sharedLocalizer[SharedResourcesKeys.UserNameIsExist]);
            //mapping
            var identityUser= _mapper.Map<User>(request);
            //create
            var CreateResult = await _userManager.CreateAsync(identityUser,request.Password);
            //failed
            if (!CreateResult.Succeeded) 
                return BadRequest<string>(CreateResult.Errors.FirstOrDefault().Description);

            //message
            //sucess
            return Created("");
        }
        #endregion
    }
}

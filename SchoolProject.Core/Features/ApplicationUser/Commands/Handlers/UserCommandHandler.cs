using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
{   public class UserCommandHandler:ResponseHandler
        ,IRequestHandler<AddUserCommand,Response<string>>
        ,IRequestHandler<EditUserCommand,Response<string>>
        ,IRequestHandler<DeleteUserCommand, Response<string>>
        ,IRequestHandler<ChangeUserPasswordCommand, Response<string>>
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

        public async Task<Response<string>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            //check if user is exist
            var Olduser=await _userManager.FindByIdAsync(request.Id.ToString());
            //if not exist not found
            if(Olduser == null) return NotFound<string>();
            //mapping
            var newUser=_mapper.Map(request,Olduser);

            //if username is Exist
            var userByUserName= await _userManager.Users.FirstOrDefaultAsync(x=>x.UserName==newUser.UserName&&x.Id!=newUser.Id);
            //username is exist
            if(userByUserName != null) return BadRequest<string>(_sharedLocalizer[SharedResourcesKeys.UserNameIsExist]);

            //update
            var result = await _userManager.UpdateAsync(newUser);
            //result is not success
            if (!result.Succeeded) return BadRequest<string>(_sharedLocalizer[SharedResourcesKeys.UpdateFailed]);
            //message
            return Success((string)_sharedLocalizer[SharedResourcesKeys.Updated]);
        }

        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            //check if user is exist
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            //if not exist not found
            if (user == null) return NotFound<string>();
            //delete the user
            var result= await _userManager.DeleteAsync(user);
            //in case of failure
            if (!result.Succeeded) return BadRequest<string>(_sharedLocalizer[SharedResourcesKeys.DeletedFailed]);
            return Success((string)_sharedLocalizer[SharedResourcesKeys.Deleted]);



        }

        public async Task<Response<string>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            //get user
            //check if user is exist
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            //if Not Exist notfound
            if (user == null) return NotFound<string>();

            //Change User Password
            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            //var user1=await _userManager.HasPasswordAsync(user);
            //await _userManager.RemovePasswordAsync(user);
            //await _userManager.AddPasswordAsync(user, request.NewPassword);

            //result
            if (!result.Succeeded) return BadRequest<string>(result.Errors.FirstOrDefault().Description);
            return Success((string)_sharedLocalizer[SharedResourcesKeys.Success]);
        }
        #endregion
    }
}

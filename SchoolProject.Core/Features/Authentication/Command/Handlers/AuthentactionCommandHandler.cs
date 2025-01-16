using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authentication.Command.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Authentication.Command.Handlers
{
    public class AuthentactionCommandHandler : ResponseHandler,
        IRequestHandler<SignInCommand, Response<JwtAuthResult>>
    {
        #region fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IAuthenicationService _authenicationService;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        #endregion
        #region ctor
        public AuthentactionCommandHandler(IAuthenicationService authenicationService, SignInManager<User> signInManager,UserManager<User> userManager,IStringLocalizer<SharedResources> stringLocalizer):base(stringLocalizer)
        {
            _authenicationService = authenicationService;
            _signInManager = signInManager;
            _userManager = userManager;
            _stringLocalizer = stringLocalizer;
        }
        #endregion
        #region functions
        public async Task<Response<JwtAuthResult>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
         //هنشةف ايميل موجود ف داتا بيز ولالا check if email is exist or not
         var Email= await _userManager.FindByEmailAsync(request.Email);
            var user = await _userManager.FindByNameAsync(Email.UserName);

         //return the email not found
         if (Email == null) return BadRequest<JwtAuthResult>(_stringLocalizer[SharedResourcesKeys.EmailIsNotExist]);
            //try to sign in
         var signInResult =  await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            //if failed return password is wrong
          if (!signInResult.Succeeded) return BadRequest<JwtAuthResult>(_stringLocalizer[SharedResourcesKeys.PasswordNotCorrect]);
            //try to sign in
            //generate token
            var result =  await _authenicationService.GetJWTToken(user);
            //return token
            return Success(result);
        }
        #endregion

    }
}

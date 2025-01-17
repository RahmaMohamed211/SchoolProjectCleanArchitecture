﻿
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Core.Features.Authentication.Command.Models;
using SchoolProject.Core.Features.Authentication.Queires.Handlers;
using SchoolProject.Core.Features.Authentication.Queires.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
    
    [ApiController]
    public class AuthenticationController :AppControllerBase
    {
        [HttpPost(Router.Authentication.SignIn)]
        public async Task<IActionResult> Create([FromForm] SignInCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);

        }
        [HttpPost(Router.Authentication.RefreshToken)]
        public async Task<IActionResult> RefreshToken([FromForm] RefreshTokenCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);

        }
        [HttpGet(Router.Authentication.ValidateToken)]
        public async Task<IActionResult> ValidateToken([FromQuery] AuthorizeUserQuery command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);

        }
    }
}

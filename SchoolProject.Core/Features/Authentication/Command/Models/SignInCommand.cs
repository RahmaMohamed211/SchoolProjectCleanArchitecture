﻿using MediatR;
using SchoolProject.Core.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Authentication.Command.Models
{
    public class SignInCommand:IRequest<Response<string>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
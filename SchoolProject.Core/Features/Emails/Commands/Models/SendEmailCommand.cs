using MediatR;
using SchoolProject.Core.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Emails.Commands.Models
{
    public class SendEmailCommand:IRequest<Response<string>>
    {
        public string Email { get; set; }
        public string Message { get; set; }
    }
}

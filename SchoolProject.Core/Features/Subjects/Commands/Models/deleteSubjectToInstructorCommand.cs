﻿using MediatR;
using SchoolProject.Core.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Subjects.Commands.Models
{
    public class deleteSubjectToInstructorCommand : IRequest<Response<string>>
    {
        public int SubId { get; set; }
        public int InsId { get; set; }
      
    
    }
}

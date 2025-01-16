using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service.Abstracts
{
    public interface IAuthenicationService
    {
        public Task< JwtAuthResult> GetJWTToken(User user);
    }
}

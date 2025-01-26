using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Data.Helpers
{
    public static class ClaimStore
    {
        public static List<Claim> claims = new List<Claim>()
        {
            new Claim("Create Student","false"),
            new Claim("Edit Student","false"),
            new Claim("Delete Student","false"),
        };
    }
}

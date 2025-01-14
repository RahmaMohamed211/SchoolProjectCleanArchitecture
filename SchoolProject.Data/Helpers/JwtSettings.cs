using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Data.Helpers
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public string Issuser { get; set; }
        public string Audience { get; set; }
        public bool ValidateIssuser { get; set; }
        public bool ValidateAudience { get; set; }
        public bool ValidateLifeTime { get; set; }
        public bool ValidateIssuserSigingKey { get; set; }
    }
}

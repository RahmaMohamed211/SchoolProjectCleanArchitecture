using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Data.Entities.Views
{
    [Keyless]
    public class ViewDepartment :GeneralLocalizableEntity
    {
        public int DId { get; set; }
        public string? DNameAr { get; set; }
        public string? DNameEn { get; set; }
        public int studentcount { get; set; }


    }
}

using SchoolProject.Data.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Data.Entities
{
    public class DepartmentSubject:GeneralLocalizableEntity
    {
        [Key]
        public int DId { get; set; }
        [Key]
        public int SubID { get; set; }

        [ForeignKey("DId")]
        [InverseProperty("DepartmentSubjects")]
        public virtual Department? Department { get; set; }

        [ForeignKey("SubID")]
        [InverseProperty("DepartmentSubjects")]
        public virtual Subject? Subjects { get; set; }

    }
}

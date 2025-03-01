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
    public class StudentSubject:GeneralLocalizableEntity
    {
        [Key]
        public int StudID { get; set; }
        [Key]
        public int SubID { get; set; }

        public decimal? grade { get; set; }

        [ForeignKey("StudID")]
        [InverseProperty("StudentSubjects")]
        public virtual Student Student { get; set; }

     

        [ForeignKey("SubID")]
        [InverseProperty("StudentSubjects")]
        public virtual Subject? Subject { get; set; }

    }
}

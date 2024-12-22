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
    public class Student:GeneralLocalizableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudID { get; set; }
        [StringLength(200)]
        public string? NameAr { get; set; }
        [StringLength(200)]
        public string? NameEn { get; set; }
        [StringLength(500)]
        public string? Address { get; set; }
        [StringLength(500)]
        public string? Phone { get; set; }


        public int? DId { get; set; }

        [ForeignKey("DId")]
        [InverseProperty("Students")]
        public virtual Department Department { get; set; }

        [InverseProperty("Student")]
        public virtual ICollection<StudentSubject> StudentSubjects { get; set; }=new HashSet<StudentSubject>();

    }
}

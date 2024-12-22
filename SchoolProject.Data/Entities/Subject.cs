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
    public class Subject : GeneralLocalizableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubID { get; set; }
        [StringLength(500)]
        public string? SubjectNameAr { get; set; }

        [StringLength(500)]
        public string? SubjectNameEn { get; set; }

        public int? Period { get; set; }

        [InverseProperty("Subject")]
        public virtual ICollection<StudentSubject> StudentSubjects { get; set; }= new HashSet<StudentSubject>();
        [InverseProperty("Subjects")]
        public virtual ICollection<DepartmentSubject> DepartmentSubjects { get; set; } = new HashSet<DepartmentSubject>();

        [InverseProperty("Subject")]
        public virtual ICollection<Ins_Subject> Ins_Subjects { get; set; } = new HashSet<Ins_Subject>();

    }
}

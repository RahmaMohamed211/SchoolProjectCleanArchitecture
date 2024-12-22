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
    public partial class Department: GeneralLocalizableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity )]
        public int DId { get; set; }
       
        public string? DNameAr { get; set; }
        [StringLength(100)]
        public string? DNameEn { get; set; }

        public int? InsManager { get; set; }

        [InverseProperty("Department")]
        public virtual ICollection<Student> Students { get; set; }=new HashSet<Student>();

        [InverseProperty("Department")]
        public virtual ICollection<DepartmentSubject> DepartmentSubjects { get; set; } =new HashSet<DepartmentSubject>();
        [InverseProperty("department")]
        public virtual ICollection<Instructor> Instructors { get; set; }=new HashSet<Instructor>();

        [ForeignKey("InsManager")]
        [InverseProperty("departmentManager")]
        public virtual Instructor? Instructor { get; set; }
    }
}

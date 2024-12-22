using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.infrastructure.Configuration
{
    public class DepartmentSubjectConfiguration : IEntityTypeConfiguration<DepartmentSubject>
    {
        public void Configure(EntityTypeBuilder<DepartmentSubject> builder)
        {


            builder.HasKey(x => new { x.SubID, x.DId });

            builder.HasOne(ds => ds.Department)
                .WithMany(d => d.DepartmentSubjects)
                .HasForeignKey(ds => ds.DId);

            builder.HasOne(ds => ds.Subjects)
                .WithMany(d => d.DepartmentSubjects)
                .HasForeignKey(ds => ds.SubID);

           

        }
    }
}

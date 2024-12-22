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
    public class StudentSubjectConfiguration : IEntityTypeConfiguration<StudentSubject>
    {
        public void Configure(EntityTypeBuilder<StudentSubject> builder)
        {

            builder.HasKey(x => new { x.SubID, x.StudID });


            builder.HasOne(ds => ds.Student)
                .WithMany(d => d.StudentSubjects)
                .HasForeignKey(ds => ds.StudID);

            builder.HasOne(ds => ds.Subject)
                .WithMany(d => d.StudentSubjects)
                .HasForeignKey(ds => ds.SubID);

         


        }
    }
}

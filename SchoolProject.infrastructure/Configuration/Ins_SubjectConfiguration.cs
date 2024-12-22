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
    public class Ins_SubjectConfiguration : IEntityTypeConfiguration<Ins_Subject>
    {
        public void Configure(EntityTypeBuilder<Ins_Subject> builder)
        {

            builder.HasKey(x => new { x.SubId, x.InsId });

            builder.HasOne(ds => ds.instructor)
                .WithMany(d => d.Ins_Subjects)
                .HasForeignKey(ds => ds.InsId);

            builder.HasOne(ds => ds.Subject)
                .WithMany(d => d.Ins_Subjects)
                .HasForeignKey(ds => ds.SubId);

            
        }
    }
}

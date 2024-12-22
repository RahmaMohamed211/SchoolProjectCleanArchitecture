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
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {

                builder.HasKey(x => x.DId);
                builder.Property(x => x.DNameAr).HasMaxLength(500);

                builder.HasMany(x => x.Students)
                       .WithOne(x => x.Department)
                       .HasForeignKey(x => x.DId)
                       .OnDelete(DeleteBehavior.Restrict);//متحذفش طلاب لو ف قسم اتشال 


                builder.HasOne(x => x.Instructor)
               .WithOne(x => x.departmentManager)
               .HasForeignKey<Department>(x => x.InsManager)
               .OnDelete(DeleteBehavior.Restrict);
            

        }
    }
}

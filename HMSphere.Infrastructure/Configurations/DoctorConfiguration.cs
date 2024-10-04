using HMSphere.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSphere.Infrastructure.Configurations
{
    internal class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.Property(p => p.Specialization)
                .HasMaxLength(50);


            builder.HasOne(d => d.Department)
                .WithMany().HasForeignKey(d => d.DeptId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(d=>d.DoctorShifts)
                .WithOne().HasForeignKey(ds=>ds.DoctorId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

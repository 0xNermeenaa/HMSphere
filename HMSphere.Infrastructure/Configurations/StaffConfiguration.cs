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
    internal class StaffConfiguration : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> builder)
        {

            builder.Property(p => p.JobTitle)
                .HasMaxLength(50);

            builder.HasOne(s=>s.Department)
                .WithMany().HasForeignKey(s=>s.DeptId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(d => d.StaffShifts)
                .WithOne().HasForeignKey(ss => ss.StaffId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

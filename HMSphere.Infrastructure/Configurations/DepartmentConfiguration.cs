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
    internal class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd().IsRequired();

            builder.Property(p => p.Name)
                .HasMaxLength(50);

            builder.Property(p => p.Description)
                .HasMaxLength(200);

            builder.Property(p=>p.Location)
                .HasMaxLength (100);

            builder.HasOne(d => d.DeptManager)
                .WithOne().HasForeignKey<Department>(d => d.ManagerId);

            builder.HasMany(d=>d.Doctors)
                .WithOne().HasForeignKey(d => d.DeptId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(s => s.Staff)
                .WithOne().HasForeignKey(s => s.DeptId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}

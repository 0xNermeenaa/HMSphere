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
    internal class ShiftConfiguration : IEntityTypeConfiguration<Shift>
    {
        public void Configure(EntityTypeBuilder<Shift> builder)
        {
            builder.Property(p => p.ID)
               .ValueGeneratedOnAdd().IsRequired();

            builder.Property(p => p.Notes)
                .HasMaxLength(200);

            builder.Property(p => p.ShiftType)
                .HasMaxLength(50);
        }
    }
}

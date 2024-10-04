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
    internal class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd().IsRequired();


            builder.Property(p => p.Clinic)
                .HasMaxLength(50);


            builder.Property(p => p.Status)
                .HasMaxLength(20);


            builder.Property(p => p.ReasonFor)
                .HasMaxLength(200);

            builder.HasOne(a => a.Patient)
                .WithMany().HasForeignKey(p => p.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Doctor)
                .WithMany().HasForeignKey(p => p.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);



        }
    }
}

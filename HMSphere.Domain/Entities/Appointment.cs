using HMSphere.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSphere.Domain.Entities
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }= DateTime.Now;
        public Status Status { get; set; } = Status.Scheduled;
        public string ReasonFor { get; set; } = string.Empty;
        public string Clinic { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;

        public string DoctorId { get; set; }
        public string PatientId { get; set; }

        public virtual Doctor Doctor { get; set; } = new();
        public virtual Patient Patient { get; set; }= new();

    }
}
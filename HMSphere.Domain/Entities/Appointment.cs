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
        public Guid ID { get; set; } = Guid.NewGuid();
        public DateTime Date { get; set; }= DateTime.Now;
        public string ReasonFor { get; set; } = string.Empty;
        public string Clinic { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;

        [ForeignKey("Doctor")]
        public string DoctorId { get; set; }

        [ForeignKey("Patient")]
        public string PatientId { get; set; }

        public virtual Doctor Doctor { get; set; } = new();

        public virtual Patient Patient { get; set; }= new();


    }
}
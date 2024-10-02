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
        public string ID { get; set; }
        public DateTime Date { get; set; }
        public string? ReasonFor { get; set; }
        public string? Clinic { get; set; }
        public string? Status { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;

        public string DoctorId { get; set; }
       // [ForeignKey("DoctorId")]
        public virtual Doctor Doctor { get; set; }
        public string PatientId { get; set; }
       // [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; }


    }
}
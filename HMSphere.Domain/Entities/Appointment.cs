using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSphere.Domain.Entities
{
    internal class Appointment
    {
        public string ID { get; set; }
        public DateTime Date { get; set; }
        public string? ReasonFor { get; set; }
        public string? Clinic { get; set; }
        public string? Status { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public string DoctorID { get; set; }
        public virtual Doctor doctor { get; set; }
        public string PatientID { get; set; }
        public virtual Patient patient { get; set; }

    }
}

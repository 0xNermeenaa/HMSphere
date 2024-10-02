using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Models
{
    public class Appointment
    {
        public int ID { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public string? ReasonFor { get; set; }
        [Required, MaxLength(10)]
        public string? Clinic { get; set; }
        [Required,MaxLength(10)]
        public string? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public int DoctorID { get; set; }
        public Doctor doctor { get; set; }
        public int PatientID { get; set; }
        public Patient patient { get; set; }


    }
}

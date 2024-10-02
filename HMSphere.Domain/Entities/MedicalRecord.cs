using HMSphere.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSphere.Domain.Entities
{
    public class MedicalRecord
    {
        public int ID { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required,MaxLength(50)]
        public string Diagnosis { get; set; }
        [Required]
        public string TreatmentPlan { get; set; }
        [Required]
        public string Medications { get; set; }
        public string DoctorNotes { get; set; }
        [Required]
        public DateTime LastUpdated { get; set; }
        public bool IsDeleted { get; set; }
        public int PatientID { get; set; }
        public Patient patient { get; set; }
        public int DoctorID { get; set; }
        public Doctor doctor { get; set; }

    }
}

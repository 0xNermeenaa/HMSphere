using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSphere.Domain.Entities
{
    internal class MedicalRecord
    {
        public string ID { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Diagnosis { get; set; }
        public string TreatmentPlan { get; set; }
        public string Medications { get; set; }
        public string DoctorNotes { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string PatientID { get; set; }
        public virtual Patient patient { get; set; }
        public string DoctorID { get; set; }
        public virtual Doctor doctor { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSphere.Domain.Entities
{
    public class Patient : ApplicationUser
    {

        public string Blood { get; set; }=string.Empty;
        public string? DiseaseHistory { get; set; }= string.Empty;
        public double Weight { get; set; }
        public double Height { get; set; }

        public virtual ICollection<Appointment>? Appointments { get; set; } = new List<Appointment>();
        public virtual ICollection<MedicalRecord>? MedicalRecords { get; set; } = new List<MedicalRecord>();

    }
}

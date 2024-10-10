using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSphere.Domain.Entities
{
    public class Doctor : ApplicationUser
    {
        public string Specialization { get; set; } = string.Empty;

        [ForeignKey("Department")]
        public Guid DeptId { get; set; }

        public virtual Department Department { get; set; } = new();
        public virtual ICollection<Appointment>? Appointments { get; set; } = new List<Appointment>();
        public virtual ICollection<DoctorShift> DoctorShifts { get; set; } = new List<DoctorShift>();
        public virtual ICollection<MedicalRecord>? MedicalRecords { get; set; } = new List<MedicalRecord>();


    }
}

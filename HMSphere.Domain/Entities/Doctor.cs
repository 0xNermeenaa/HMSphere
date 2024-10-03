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
        public string DeptId { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();





    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSphere.Domain.Entities
{
    public class Shift
    {
        public string ID { get; set; }
        public DateTime ShiftDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Notes { get; set; }
        public string ShiftType { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; } = false;
        public virtual ICollection<Doctor> Doctors { get; set; }
        public virtual ICollection<Staff> Staff { get; set; }
    }
}
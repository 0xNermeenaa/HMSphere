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
        public Guid ID { get; set; }= Guid.NewGuid();
        public DateTime ShiftDate { get; set; }= DateTime.Now;
        public DateTime StartTime { get; set; }=DateTime.Now;
        public DateTime EndTime { get; set; }= DateTime.Now;
        public string Notes { get; set; }= string.Empty;
        public string ShiftType { get; set; }=string.Empty;
        public bool IsActive { get; set; }=false;
        public bool IsDeleted { get; set; } = false;
        public virtual ICollection<Doctor> Doctors { get; set; }
        public virtual ICollection<Staff> Staff { get; set; }
    }
}
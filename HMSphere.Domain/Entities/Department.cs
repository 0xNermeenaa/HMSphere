using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HMSphere.Domain.Entities
{
    internal class Department
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string Location { get; set; }
        public string ManagerID { get; set; }
        //[ForeignKey("ManagerID")]
        public virtual Doctor DeptManager { get; set; }
        public virtual ICollection<Doctor> doctors { get; set; }
        public virtual ICollection<Staff> Staff { get; set; }
    }
}

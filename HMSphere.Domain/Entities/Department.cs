using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HMSphere.Domain.Entities
{
    public class Department
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string Location { get; set; }
        public int ManagerID { get; set; }
       // [ForeignKey("ManagerID")]
        public virtual Doctor DeptManager { get; set; }
        public virtual ICollection<Doctor> doctors { get; set; }
        public virtual ICollection<Staff> Staff { get; set; }
    }
}
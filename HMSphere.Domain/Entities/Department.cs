using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HMSphere.Domain.Entities
{
    public class Department
    {
        public Guid ID { get; set; }= Guid.NewGuid();
        public string Name { get; set; }= string.Empty;
        public string Description { get; set; }=string.Empty;
        public bool IsActive { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
        public string Location { get; set; } = string.Empty;

        [ForeignKey("DeptManager")]
        public int ManagerID { get; set; }

        public virtual Doctor DeptManager { get; set; } = new();
        public virtual ICollection<Doctor> Doctors { get; set; }= new List<Doctor>();
        public virtual ICollection<Staff> Staff { get; set; }=new List<Staff>();
    }
}
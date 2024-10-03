using HMSphere.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;


namespace HMSphere.Domain.Entities
{
    public class Staff : ApplicationUser
    {

        public Role Role { get; set; }
        public string JobTitle { get; set; } = string.Empty;
        public DateTime HireDate { get; set; } = DateTime.Now;

        [ForeignKey("Department")]
        public string DeptId { get; set; }
        [ForeignKey("Shift")]
        public string ShiftId { get; set; }

        public virtual Department Department { get; set; } = new();
        public virtual Shift Shift { get; set; } = new();




    }
}

using HMSphere.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;


namespace HMSphere.Domain.Entities
{
    public class Staff : ApplicationUser
    {
        public Role Role { get; set; }
        public string JobTitle { get; set; } = string.Empty;
        public DateOnly HireDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        [ForeignKey("Department")]
        public Guid DeptId { get; set; }

        public virtual Department Department { get; set; } = new();
        public virtual ICollection<StaffShift> StaffShifts { get; set; } = new List<StaffShift>();

    }
}

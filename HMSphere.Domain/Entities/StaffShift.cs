using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSphere.Domain.Entities
{
    public class StaffShift
    {
        public Guid Id { get; set; }= Guid.NewGuid();
        public bool IsDeleted { get; set; } = false;

        [ForeignKey("Staff")]
        public string StaffId { get; set; }

        [ForeignKey("Shift")]
        public Guid ShiftId { get; set; }


        public virtual Staff Staff { get; set; } = new();
        public virtual Shift Shift { get; set; } = new();

    }
}

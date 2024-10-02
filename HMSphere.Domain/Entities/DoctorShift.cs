using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSphere.Domain.Entities
{
    internal class DoctorShift
    {
        [Required]
        public int Id { get; set; }

        // Foreign Key for class Doctor
        [Required]
        public int DoctorId { get; set; }

        // Foreign Key for class Shift
        [Required]
        public int ShiftId { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}

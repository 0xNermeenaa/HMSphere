using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSphere.Domain.Entities
{
    public class Doctor : ApplicationUser
    {
        //[Required]
        //[StringLength(100)]
        public string Specialization { get; set; }


        //[Required]
        // Foreign Key for ApplicationUSer
        //public int AppUserId { get; set; }


        //[Required]
        // Foreign Key for DeptId
        public int DeptId { get; set; }

        // Foreign Key for Shift 
        //[Required]
        public int ShiftId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSphere.Domain.Entities
{
    internal class Staff
    {
        //[Required]
        //[StringLength(50)]
        public string Role { get; set; }


        //[Required]
        //[StringLength(100)]
        public string JobTitle { get; set; }


        //[Required]
        //[DataType(DataType.Date)]
        public DateTime HireDate { get; set; }

        // Foreign Key for DeptId
        //[Required]
        public int DeptId { get; set; }


        // Foreign Key for Shift 
        //[Required]
        public int ShiftId { get; set; }
    }
}

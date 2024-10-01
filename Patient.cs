using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSphere.Domain.Entities
{
    internal class Patient
    {
        // Blood type(A+, B-, O+ , ..)
        //[Required]
        //[StringLength(5)]
        public string Blood { get; set; }

        //[StringLength(2000)]
        public string DiseaseHistory { get; set; }

        //[Required]
        public double Weight { get; set; }

        //[Required]
        public double Height { get; set; }
    }
}

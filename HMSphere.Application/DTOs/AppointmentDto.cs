using HMSphere.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSphere.Application.DTOs
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } 
        public Status Status { get; set; }
        public string ReasosnFor { get; set; } 
        public string Clinic { get; set; } 
        public DateTime CreatedDate { get; set; } 
    }
}

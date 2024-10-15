using HMSphere.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSphere.Application.DTOs
{
	public class AppointmentDto
	{
		public DateTime Date { get; set; }
		public Status Status { get; set; }
		public string ReasonFor { get; set; }
		public string Clinic { get; set; }
		public DateTime CreatedDate { get; set; }
		public bool IsDeleted { get; set; }
		public string DoctorID { get; set; }
		public string PatientID { get; set; }
	}
}

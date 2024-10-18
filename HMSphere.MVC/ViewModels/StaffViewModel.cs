using HMSphere.Domain.Entities;

namespace HMSphere.MVC.ViewModels
{
	public class StaffViewModel
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Department { get; set; }
		public string JobTitle { get; set; }
		public DateOnly? HireDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
		public string StaffShift { get; set; }
	}
}
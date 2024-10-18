using HMSphere.Domain.Entities;
using HMSphere.Domain.Enums;

namespace HMSphere.MVC.ViewModels
{
    public class StaffViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
		public string LastName { get; set; }
		public string JobTitle { get; set; }
		public string Department { get; set; }
		public string PhoneNumber { get; set; }

	}
}

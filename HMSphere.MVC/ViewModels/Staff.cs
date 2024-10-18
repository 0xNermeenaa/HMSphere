using HMSphere.Domain.Entities;
using HMSphere.Domain.Enums;

namespace HMSphere.MVC.ViewModels
{
    public class Staff
    {
		
			public string Id { get; set; } // will be same as AppUser Id
			public string JobTitle { get; set; } = string.Empty;
			public int DepartmentId { get; set; }


	}
}

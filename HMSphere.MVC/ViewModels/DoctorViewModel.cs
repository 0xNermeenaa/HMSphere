using System.ComponentModel.DataAnnotations;

namespace HMSphere.MVC.ViewModels
{
    public class DoctorViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Specialization { get; set; }
        public string Department { get; set; }
        public int UpcomingAppointmentsCount { get; set; }
    }
}

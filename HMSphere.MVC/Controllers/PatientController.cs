using HMSphere.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HMSphere.MVC.Controllers
{
    public class PatientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Appointments()
        {
            return View();
        }
        public IActionResult MedicalRecords()
        {
            return View();
        }
        public IActionResult AppointmentDetails()
        {
            return View();
        }

        public IActionResult MedicalRecordDetails()
        {
            return View();
        }
    }
}

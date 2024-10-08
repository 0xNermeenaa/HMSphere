using Microsoft.AspNetCore.Mvc;

namespace HMSphere.MVC.Controllers
{
    public class DoctorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Patients()
        {
            return View();
        }

        public IActionResult Appointments()
        {
            return View();
        }
    }
}

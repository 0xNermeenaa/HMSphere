using Microsoft.AspNetCore.Mvc;

namespace HMSphere.MVC.Controllers
{
    public class DoctorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

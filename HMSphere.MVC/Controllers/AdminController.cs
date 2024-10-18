using HMSphere.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HMSphere.MVC.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Doctors()
        {
            return View();
        }
        public IActionResult Patients()
        {
            return View();
        }
        public IActionResult Staff()
        {
            return View();
        }
    }
  
}

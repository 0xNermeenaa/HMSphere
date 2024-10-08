using HMSphere.Application.Interfaces;
using HMSphere.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HMSphere.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register(RegisterViewModel model)
        {
            return View();
        }
        public IActionResult Login(LoginViewModel model)
        {
            return View();
        }
    }
}

using HMSphere.Application.Interfaces;
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
    }
}

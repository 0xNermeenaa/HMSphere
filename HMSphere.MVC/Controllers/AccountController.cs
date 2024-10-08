using HMSphere.Application.DTOs;
using HMSphere.Application.Interfaces;
using HMSphere.Domain.Entities;
using HMSphere.MVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        public IActionResult Login()
        {
            return View("Login");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]//requets.form['_requetss]
        public async Task<IActionResult> SaveLogin(LoginViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                var loginDto = new LoginDto
                {
                    Email = userViewModel.Email,
                    Password = userViewModel.Password
                };

                var authResult = await _accountService.LoginAsync(loginDto);

                if (authResult.IsAuthenticated)
                {
                    return RedirectToAction("Index", "Doctor");
                }
                else
                {
                    ModelState.AddModelError("", authResult.Message ?? "Username or Password is incorrect");
                }
            }

            return View("Login", userViewModel);
        }
    }
}

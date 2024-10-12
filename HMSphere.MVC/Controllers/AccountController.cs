using AutoMapper;
using HMSphere.Application.DTOs;
using HMSphere.Application.Interfaces;
using HMSphere.Domain.Entities;
using HMSphere.MVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace HMSphere.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;
        public AccountController(IAccountService accountService, IMapper mapper,
                                   IDepartmentService departmentService)
        {
            _accountService = accountService;
            _mapper = mapper;
            _departmentService = departmentService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Register()
        {
            //ViewData["Departments"] = new SelectList(await _departmentService.GetDepartments(), "Id", "Name");

            return View("Register");
        }


        [HttpPost]
        public async Task<IActionResult> SaveRegister(RegisterViewModel userViewModel)
        {

            if (ModelState.IsValid)
            {
                var registerDto = _mapper.Map<RegisterDto>(userViewModel);

                var authResult = await _accountService.RegisterAsync(registerDto);

                if (authResult.IsAuthenticated)
                {
                    return RedirectToAction("Index", "Doctor");
                }
                else
                {
                    ModelState.AddModelError("", authResult.Message ?? "Username or Password is incorrect");
                }
            }
            //ViewData["Departments"] = new SelectList(await _departmentService.GetDepartments(), "Id", "Name");

            return View("Register", userViewModel);
     
                
            
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

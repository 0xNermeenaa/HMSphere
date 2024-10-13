using AutoMapper;
using HMSphere.Application.DTOs;
using HMSphere.Application.Interfaces;
using HMSphere.Application.Services;
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
        private readonly IUserRoleFactory _userRoleFactory;
		private readonly UserManager<ApplicationUser> _userManager;

		public AccountController(IAccountService accountService, IMapper mapper,
								   IDepartmentService departmentService, IUserRoleFactory userRoleFactory, UserManager<ApplicationUser> userManager)
		{
			_accountService = accountService;
			_mapper = mapper;
			_departmentService = departmentService;
			_userRoleFactory = userRoleFactory;
			_userManager = userManager;
		}
		public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Register()
        {
            ViewData["Departments"] = new SelectList(await _departmentService.GetDepartments(), "Id", "Name");

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
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("", authResult.Message ?? "Username or Password is incorrect");
                }
            }
            ViewData["Departments"] = new SelectList(await _departmentService.GetDepartments(), "Id", "Name");

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
            var currentUser = await _accountService.GetCurrentUser(userViewModel.Email);

			if (ModelState.IsValid)
            {
                var loginDto = new LoginDto
                {
                    Email = userViewModel.Email,
                    Password = userViewModel.Password
                };

                var authResult = await _accountService.LoginAsync(loginDto);
                var roleRedirects = _userRoleFactory.roleRedirects;

				if (authResult.IsAuthenticated)
                {
                    foreach(var role in roleRedirects)
                    {
                        if (currentUser != null)
                        {
						    if (await _userManager.IsInRoleAsync(currentUser, role.Key))
						    {
							    return RedirectToAction(role.Value.action, role.Value.controller);
						    }
                        }
					}
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

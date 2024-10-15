using AutoMapper;
using HMSphere.Application.DTOs;
using HMSphere.Application.Interfaces;
using HMSphere.Application.Services;
using HMSphere.Domain.Entities;
using HMSphere.Domain.Enums;
using HMSphere.MVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HMSphere.MVC.Controllers
{
    public class PatientController : Controller
    {
        private readonly IDoctorService _doctorService;
        private readonly IDepartmentService _departmentService;
        private readonly IAppointmentService _appointmentService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public PatientController(IDoctorService doctorService,
                                IDepartmentService departmentService,
                                IAppointmentService appointmentService,
                                IMapper mapper,
                                UserManager<ApplicationUser> userManager)
        {
            _doctorService = doctorService;
            _departmentService = departmentService;
            _appointmentService = appointmentService;
            _mapper = mapper;
            _userManager = userManager;
        }
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

        public async Task<IActionResult> CreateAppointment()
        {
            var userId = _userManager.GetUserId(User); // Get the current logged-in user's ID
            ViewBag.CurrentUserId = userId;

            ViewData["Departments"] = new SelectList(await _departmentService.GetDepartments(), "Id", "Name");
            ViewData["Doctors"] = new SelectList(await _doctorService.GetDoctorsByDepartmentIdAsync(null), "Id", "User.UserName");
            //ViewBag.StatusList = new SelectList(Enum.GetValues(typeof(Status))
            //                    .Cast<Status>()
            //                    .Select(s => new { Value = s.ToString(), Text = s.ToString() }),
            //                    "Value", "Text");

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateAppointment(AppointmentViewModel model)
        {

            if (ModelState.IsValid)
            {

                var appointmentDto = _mapper.Map<AppointmentDto>(model);

                var result = await _appointmentService.CreateAppointment(appointmentDto);
                if (!result.IsSuccessful)
                {
                    ModelState.AddModelError(string.Empty, result.ErrorMessage);
                    return View("CreateAppointment",model);
                }

                return RedirectToAction("Index");
            }
            ViewData["Departments"] = new SelectList(await _departmentService.GetDepartments(), "Id", "Name");
            ViewData["Doctors"] = new SelectList(await _doctorService.GetDoctorsByDepartmentIdAsync(model.DepartmentId), "Id", "User.UserName");
           
            return View("CreateAppointment", model);



        }



        public async Task<JsonResult> GetDoctorsByDepartment(int? departmentId)
        {
            var doctors = await _doctorService.GetDoctorsByDepartmentIdAsync(departmentId);
            return Json(new SelectList(doctors, "Id", "User.UserName"));
        }
    }
}

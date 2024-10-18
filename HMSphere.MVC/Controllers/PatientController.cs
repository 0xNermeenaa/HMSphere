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
        private readonly IPatientService _patientService;

        private readonly UserManager<ApplicationUser> _userManager;

        public PatientController(IDoctorService doctorService,
                                IDepartmentService departmentService,
                                IAppointmentService appointmentService,
                                IMapper mapper,
                                UserManager<ApplicationUser> userManager,
                                IPatientService patientService)
        {
            _doctorService = doctorService;
            _departmentService = departmentService;
            _appointmentService = appointmentService;
            _mapper = mapper;
            _userManager = userManager;
            _patientService = patientService;
        }
        public async Task<IActionResult> Index(string id)
        {
            var currentUser=await _userManager.GetUserAsync(User);
            if(id == null)
            {
                return NotFound();
            }
            var lastFiveAppointments = await _patientService.GetLast5AppointmentsAsync(id);

            var lastFiveMedicalRecords = await _patientService.GetLast5MedicalRecordsAsync(id);

            return View();
        }
        public async Task<IActionResult> Appointments()
        {
            List<PatientAppointmentsViewModel> models = new();
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return NotFound();
            }
            var appointments = await _patientService.GetAllAppointmentsAsync(currentUser.Id);
            foreach (var appointment in appointments)
            {
                var model=_mapper.Map<PatientAppointmentsViewModel>(appointment);
                models.Add(model);
            }
            return View(models);
        }
        public async Task<IActionResult> MedicalRecords()
        {
            List<PatientMedicalRecordsViewModel> models = new();
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return NotFound();
            }
            var medicalrecords = await _patientService.GetAllMedicalRecordsAsync(currentUser.Id);
            foreach (var record in medicalrecords)
            {
                var model=_mapper.Map<PatientMedicalRecordsViewModel>(record);
                models.Add(model);
            }
            return View(models);
        }

        public async Task<IActionResult> CreateAppointment()
        {
            ViewData["Departments"] = new SelectList(await _departmentService.GetDepartments(), "Id", "Name");
            ViewData["Doctors"] = new SelectList(await _doctorService.GetDoctorsByDepartmentIdAsync(null), "Id", "User.UserName");


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

                return RedirectToAction("Appointments");
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

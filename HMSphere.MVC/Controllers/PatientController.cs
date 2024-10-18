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
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return NotFound();
            }
            else
            {
                var lastFiveAppointments = await _patientService.GetLast5AppointmentsAsync(currentUser.Id);

                var lastFiveMedicalRecords = await _patientService.GetLast5MedicalRecordsAsync(currentUser.Id);

                var patientViewModel = new PatientViewModel
                {
                    Last5Appointments = (List<AppointmentsViewModel>)lastFiveAppointments,
                    Last5MedicalRecords = (List<MedicalRecordViewModel>)lastFiveMedicalRecords
                };

                return View(patientViewModel);
            }
        }
        public async Task<IActionResult> Appointments()
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User is not authenticated.");
            }

            var appointmentDtos = await _appointmentService.GetAllAppointmentsByPatientIdAsync(userId);

            if (appointmentDtos == null || !appointmentDtos.Any())
            {
                return View();
            }

            var appointmentViewModels = appointmentDtos.Select(dto => new AppointmentViewModel
            {
                Id = dto.Id,
                Date = dto.Date,
                Status = dto.Status,
                ReasonFor = dto.ReasonFor,
                Clinic = dto.Clinic,
                PatientId = dto.PatientId,
                DepartmentId = dto.DepartmentId,
                AppointmentTime = dto.AppointmentTime,
                DoctorId = dto.DoctorId,
                PatientName = dto.PatientName,
                DoctorName = dto.DoctorName
            }).ToList();

            return View(appointmentViewModels);
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
        public async Task<IActionResult> AppointmentDetails(int id)
        {
            if (id == 0)
            {
                return BadRequest("Invalid appointment ID.");
            }

            var appointment = await _appointmentService.GetAppointmentByIdAsync(id);

            if (appointment == null)
            {
                return NotFound("Appointment not found.");
            }

            var viewModel = new AppointmentViewModel
            {
                Id = appointment.Id,
                Date = appointment.Date,
                AppointmentTime = appointment.AppointmentTime,
                PatientName = appointment.PatientName,
                DoctorName = appointment.DoctorName,
                ReasonFor = appointment.ReasonFor,
            };

            return View(viewModel);
        }


        public IActionResult MedicalRecordDetails()
        {
            return View();
        }
    }
}

using AutoMapper;
using HMSphere.Application.Interfaces;
using HMSphere.MVC.ViewModels;
using HMSphere.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using HMSphere.Application.DTOs;
using HMSphere.Application.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HMSphere.MVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IMapper _mapper;
        private readonly IPatientService _patientService;
        private readonly IDoctorService _doctorService;
        private readonly IStaffService _staffService;
        private readonly IDepartmentService _departmentService;
        public AdminController(IAppointmentService appointmentService, IMapper mapper,
            IPatientService patientService, IDoctorService doctorService, IStaffService staffService, IDepartmentService departmentService)
        {
            _appointmentService = appointmentService;
            _mapper = mapper;
            _patientService = patientService;
            _doctorService = doctorService;
            _staffService = staffService;
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ApproveAppointment(int id, bool isApproved)
        {
            var result = await _appointmentService.ApproveAppointment(id, isApproved);
            if (!result)
            {
                // Handle error (e.g., appointment not found)
                return NotFound();
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Doctors()
        {
            var doctors = await _doctorService.GetAll();
            if (!doctors.Any())
            {
                return View();
            }
            var model = doctors.Select(d => _mapper.Map<DoctorViewModel>(d)).ToList();
            return View(model);
        }

        public async Task<IActionResult> PendingAppointments()
        {
            var pendingAppointments = await _appointmentService.GetPendingAppointments();

            var appointmentsViewModels = _mapper.Map<List<AppointmentViewModel>>(pendingAppointments);

            return View(appointmentsViewModels);
        }

        public async Task<IActionResult> Patients()
        {
            var patients = await _patientService.GetAll();
            if (!patients.Any())
            {
                return View();
            }
            var model = patients.Select(p => _mapper.Map<PatientsHistoryViewModel>(p)).ToList();
            return View(model);
        }

        public async Task<IActionResult> Staff()
        {
            var staff = await _staffService.GetAllAsync();
            var model = staff.Select(s => _mapper.Map<StaffViewModel>(s)).ToList();
            return View(model);
        }

        public async Task<IActionResult> MedicalRecords(string? patientId)
        {
            if (string.IsNullOrEmpty(patientId))
            {
                return BadRequest("Patient ID is required.");
            }

            var medicalRecords = await _doctorService.GetAllMedicalRecordsAsync(patientId);
            if (medicalRecords == null || !medicalRecords.Any())
            {
                return NotFound("No medical records found for the provided patient ID.");
            }
            var model = medicalRecords.Select(m => _mapper.Map<MedicalRecordViewModel>(m)).ToList();
            return View(model);
        }
        public IActionResult AddDoctor()
        {
            return View();
        }
        public async Task<IActionResult> CreateAppointment()
        {
            ViewData["Patients"] = new SelectList(await _patientService.GetPatients(), "Id", "User.UserName");

            ViewData["Doctors"] = new SelectList(await _doctorService.GetDoctorsByDepartmentIdAsync(null), "Id", "User.UserName");


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointment(AppointmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var appointmentDto = _mapper.Map<AppointmentDto>(model);

                var result = await _appointmentService.CreateAppointmentByAdmin(appointmentDto);
                if (!result.IsSuccessful)
                {
                    ModelState.AddModelError(string.Empty, result.ErrorMessage);
                    return View("CreateAppointment", model);
                }

                return RedirectToAction("Appointments");
            }
            ViewData["Patients"] = new SelectList(await _patientService.GetAll(), "Id", "User.UserName");
            ViewData["Doctors"] = new SelectList(await _doctorService.GetDoctorsByDepartmentIdAsync(model.DepartmentId), "Id", "User.UserName");

            return View("CreateAppointment", model);



        }
    }

}

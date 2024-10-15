using AutoMapper;
using HMSphere.Application.Interfaces;
using HMSphere.Application.Services;
using HMSphere.Domain.Entities;
using HMSphere.Infrastructure.Repositories;
using HMSphere.MVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HMSphere.MVC.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;
		private readonly IBaseRepository<MedicalRecord> _baseRepository;
		private readonly IMapper _mapper;
		private readonly IAppointmentService _appointmentService;
        private readonly UserManager<ApplicationUser> _userManager;


        public DoctorController(IDoctorService doctorService, IBaseRepository<MedicalRecord> baseRepository, IMapper mapper, IAppointmentService appointmentService, UserManager<ApplicationUser> userManager)
        {
            _doctorService = doctorService;
            _baseRepository = baseRepository;
            _mapper = mapper;
            _appointmentService = appointmentService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            var response = await _doctorService.Profile(id);
            if (response.IsSuccess)
            {
                var doctor = _mapper.Map<DoctorViewModel>(response.Model);
				if (doctor != null)
					return View(doctor);
                return NotFound();
            }
            return NotFound();
        }

        public IActionResult AppointmentDetails()
        {
            return View();
        }

		public async Task<IActionResult> PatientHistory()
		{
			var currentUser=await _userManager.GetUserAsync(User);
			if(currentUser == null)
			{
				return NotFound();
			}

			var patients = await _doctorService.GetAllPatientAsync(currentUser.Id);

			return View("PatientHistory");
		}

		public async Task<IActionResult> Appointments()
		{

			return View();
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
			return View();
		}

	}
}

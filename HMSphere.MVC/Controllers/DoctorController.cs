using HMSphere.Application.Helpers;
using HMSphere.Application.Interfaces;
using HMSphere.Domain.Entities;
using HMSphere.Infrastructure.Repositories;
using HMSphere.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HMSphere.MVC.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorService _repo;
		private readonly IUserHelpers _userHelper;
		private readonly IAppointmentService _appointmentService;
		private readonly IBaseRepository<MedicalRecord> _baseRepository;
		public DoctorController(IDoctorService repo, IBaseRepository<MedicalRecord> baseRepository
			,IAppointmentService appointmentService , IUserHelpers userHelpers)
		{
			_repo = repo;
			_baseRepository = baseRepository;
			_appointmentService = appointmentService;
			_userHelper = userHelpers;
		}

		public IActionResult Index()
        {
            return View();
        }

		public async Task<IActionResult> PatientHistory(string? doctorId/* ممكن منحتاجش لده واجيبه من الكرنت يوزر */)
		{
			if (string.IsNullOrEmpty(doctorId))
			{
				return BadRequest("Doctor ID is required.");
			}

			var patients = await _repo.GetAllPatientAsync(doctorId);

			//if (patients == null || !patients.Any())
			//{
			//	return NotFound("No patients found for the provided doctor ID.");
			//}

			return View(/*patients*/);
		}

		public async Task<IActionResult> Appointments()
		{
			if (await _userHelper.GetCurrentUserAsync()==null)
			{
				return Unauthorized("User is not authenticated.");
			}

			var appointments = await _appointmentService.GetAllAppointmentsAsync();
			var appointmentViewModels = appointments.Select(a => new AppointmentsViewModel
			{
				Date = a.Date,
				Status = a.Status,
				ReasonFor = a.ReasonFor,
				Clinic = a.Clinic,
				CreatedDate = a.CreatedDate,
				IsDeleted = a.IsDeleted,
				DoctorID = a.DoctorId,
				PatientID = a.PatientId,
				PatientName = $"{a.Patient.User.FirstName} {a.Patient.User.LastName}"
			}).ToList();

			if (appointments == null || !appointments.Any())
			{
				return NotFound("No Patients Found for the provided Doctor ID.");
			}

			return View(appointmentViewModels);
		}


		public async Task<IActionResult> MedicalRecords(string? patientId)
		{
			if (string.IsNullOrEmpty(patientId))
			{
				return BadRequest("Patient ID is required.");
			}

			var medicalRecords = await _repo.GetAllMedicalRecordsAsync(patientId);
			if (medicalRecords == null || !medicalRecords.Any())
			{
				return NotFound("No medical records found for the provided patient ID.");
			}

			return View(medicalRecords); // Ensure you're returning the view with data
		}

	}
}

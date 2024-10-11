using HMSphere.Application.Interfaces;
using HMSphere.Domain.Entities;
using HMSphere.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HMSphere.MVC.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorService _repo;
		private readonly IBaseRepository<MedicalRecord> _baseRepository;
		public DoctorController(IDoctorService repo, IBaseRepository<MedicalRecord> baseRepository)
		{
			_repo = repo;
			_baseRepository = baseRepository;
		}

		public IActionResult Index()
        {
            return View();
        }

		public async Task<IActionResult> PatientHistory(string? doctorId)
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

		public IActionResult Appointments()
        {
            return View();
        }

		public async Task<IActionResult> MedicalRecords(string? patientId)
		{
			if(string.IsNullOrEmpty(patientId))
			{
				return BadRequest("Patient ID is required.");

			}
			var medicalRecords = await _repo.GetAllMedicalRecordsAsync(patientId);
			return View();
		}
    }
}

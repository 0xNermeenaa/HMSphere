using HMSphere.Application.Interfaces;
using HMSphere.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HMSphere.MVC.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorService _repo;
		public DoctorController(IDoctorService repo)
		{
			_repo = repo;
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
    }
}

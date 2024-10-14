using AutoMapper;
using HMSphere.Application.Interfaces;
using HMSphere.Application.Services;
using HMSphere.Domain.Entities;
using HMSphere.Infrastructure.Repositories;
using HMSphere.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HMSphere.MVC.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;
		private readonly IBaseRepository<MedicalRecord> _baseRepository;
		private readonly IMapper _mapper;


        public DoctorController(IDoctorService doctorService, IBaseRepository<MedicalRecord> baseRepository, IMapper mapper)
        {
            _doctorService = doctorService;
            _baseRepository = baseRepository;
            _mapper = mapper;
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

        public async Task<IActionResult> PatientHistory(string? doctorId)
		{
			if (string.IsNullOrEmpty(doctorId))
			{
				return BadRequest("Doctor ID is required.");
			}

			var patients = await _doctorService.GetAllPatientAsync(doctorId);

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
			var medicalRecords = await _doctorService.GetAllMedicalRecordsAsync(patientId);
			return View();
		}
    }
}

using AutoMapper;
using HMSphere.Application.Interfaces;
using HMSphere.MVC.ViewModels;
using HMSphere.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using HMSphere.Infrastructure.Repositories;
using HMSphere.Application.DTOs;

namespace HMSphere.MVC.Controllers
{
	public class AdminController : Controller
	{
		private readonly IAppointmentService _appointmentService;
		private readonly IMapper _mapper;
		private readonly IPatientService _patientService;
		private readonly IDoctorService _doctorService;
		private readonly IStaffService _staffService;
		private readonly IBaseRepository<Shift> _shiftRepo;

		public AdminController(IAppointmentService appointmentService, IMapper mapper, IBaseRepository<Shift> shiftRepo,
			IPatientService patientService, IDoctorService doctorService, IStaffService staffService)
		{
			_appointmentService = appointmentService;
			_mapper = mapper;
			_patientService = patientService;
			_doctorService = doctorService;
			_staffService = staffService;
			_shiftRepo = shiftRepo;
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
			if (!staff.Any())
			{
				return View();
			}
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
        public IActionResult UpdateDoctor()
        {
            return View();
        }
        public IActionResult UpdatePatient()
        {
            return View();
        }
        public IActionResult UpdateStaff()
        {
            return View();
        }
		[HttpPost]
		public async Task<IActionResult> AddShift(ShiftDto newShift)
		{
			if (!ModelState.IsValid)
			{
				return RedirectToAction("Shifts");
			}

			var shiftEntity = _mapper.Map<Shift>(newShift);
			await _shiftRepo.AddAsync(shiftEntity);
			return RedirectToAction("Shifts");
		}
		[HttpPost]
		public async Task<IActionResult> AssignStaffToShift(int shiftId, string staffId)
		{
			//var result = await _staffService.AssignStaffToShiftAsync(shiftId, staffId);
			//if (!result)
			//{
			//	return NotFound();
			//}
			return RedirectToAction("Staff");
		}

		[HttpPost]
		public async Task<IActionResult> AssignDoctorToShift(int shiftId, string doctorId)
		{
			//var result = await _staffService.AssignDoctorToShiftAsync(shiftId, doctorId);
			//if (!result)
			//{
			//	return NotFound();
			//}
			return RedirectToAction("Doctors");
		}

		public async Task<ActionResult> Shifts()
		{
			var shifts = await _shiftRepo.GetAllAsync();
			var shiftDtos = shifts.Select(shift => _mapper.Map<ShiftDto>(shift)).ToList();
			var shiftViewModels = shiftDtos.Select(shift => _mapper.Map<ShiftViewModel>(shift)).ToList();
			var model = new ShiftManagementViewModel
			{
				Shifts = shiftViewModels,
				NewShift = new ShiftDto()
			};

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> DeleteShift(int id)
		{
			var shift = await _shiftRepo.GetByIdAsync(id); // Fetch by ID
			if (shift == null)
			{
				return NotFound(); // Handle not found
			}
			await _shiftRepo.DeleteAsync(shift); // Delete the shift    
			TempData["Message"] = "Shift deleted successfully.";

			return RedirectToAction("Shifts"); // Redirect after deletion
		}
	}
}

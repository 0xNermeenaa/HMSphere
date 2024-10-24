using AutoMapper;
using HMSphere.Application.Interfaces;
using HMSphere.MVC.ViewModels;
using HMSphere.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using HMSphere.Application.DTOs;
using HMSphere.Application.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using HMSphere.Infrastructure.Repositories;

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
		private readonly IBaseRepository<Staff> _staffRepo;
		private readonly IBaseRepository<Doctor> _doctorRepo;
		private readonly IBaseRepository<Patient> _patientRepo;


		public AdminController(IAppointmentService appointmentService, IMapper mapper, IBaseRepository<Shift> shiftRepo,
			IBaseRepository<Staff> staffRepo, IBaseRepository<Doctor> docotrRepo, IBaseRepository<Patient> patientRepo,
			IPatientService patientService, IDoctorService doctorService, IStaffService staffService)
		{
			_appointmentService = appointmentService;
			_mapper = mapper;
			_patientService = patientService;
			_doctorService = doctorService;
			_staffService = staffService;
			_shiftRepo = shiftRepo;
			_doctorRepo = docotrRepo;
			_staffRepo = staffRepo;
			_patientRepo = patientRepo;
		}

		public async Task<IActionResult> Index()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (string.IsNullOrEmpty(userId))
			{
				return NotFound();
			}
			var dto=await _staffService.GetById(userId);
			var model=_mapper.Map<StaffViewModel>(dto);
			return View(model);
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
		public async Task<IActionResult> DeleteStaff(int id)
		{
			var staff = await _staffRepo.GetByIdAsync(id);
			if (staff == null)
			{
				return NotFound();
			}
			await _staffRepo.DeleteAsync(staff);  
			TempData["Message"] = "Staff deleted successfully.";

			return RedirectToAction("Staff");
		}

		[HttpPost]
		public async Task<IActionResult> DeleteDoctor(int id)
		{
			var doctor = await _doctorRepo.GetByIdAsync(id);
			if (doctor == null)
			{
				return NotFound();
			}
			await _doctorRepo.DeleteAsync(doctor);
			TempData["Message"] = "Doctor deleted successfully.";

			return RedirectToAction("Doctors");
		}
		[HttpPost]
		public async Task<IActionResult> DeletePatient(int id)
		{
			var patient = await _patientRepo.GetByIdAsync(id);
			if (patient == null)
			{
				return NotFound();
			}
			await _patientRepo.DeleteAsync(patient);
			TempData["Message"] = "Patient deleted successfully.";

			return RedirectToAction("Patients");
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

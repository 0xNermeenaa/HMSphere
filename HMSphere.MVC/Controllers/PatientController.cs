using AutoMapper;
using HMSphere.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using HMSphere.Application.Services;
using HMSphere.Application.Interfaces;
using AutoMapper;
using HMSphere.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace HMSphere.MVC.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public PatientController(IPatientService patientService, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _patientService = patientService;
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string id)
        {
            if(id == null)
            {
                return BadRequest("PatientID is missing.");
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
    }
}

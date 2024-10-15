using AutoMapper;
using HMSphere.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using HMSphere.Application.Services;
using HMSphere.Application.Interfaces;
using AutoMapper;
using HMSphere.Domain.Entities;

namespace HMSphere.MVC.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;
        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }
        public async Task<IActionResult> Index()
        {
            //var lastFiveAppointments = await _patientService.GetLast5AppointmentsAsync(patientId);

            //var lastFiveMedicalRecords = await _patientService.GetLast5MedicalRecordsAsync(patientId);

            return View();
        }
        public async Task<IActionResult> Appointments(string? patientId)
        {   
            if(patientId == null)
            {
                return BadRequest("Patient ID is missing.");
            }
            var appointments = await _patientService.GetAllAppointmentsAsync(patientId);
            return View();
        }
        public IActionResult MedicalRecords(string? patientId)
        {

            if (patientId == null)
            {
                return BadRequest("Patient ID is missing.");
            }
            var medicalrecords = _patientService.GetAllMedicalRecordsAsync(patientId);
            return View();
        }
    }
}

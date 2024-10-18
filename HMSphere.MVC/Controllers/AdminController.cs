using AutoMapper;
using HMSphere.Application.Interfaces;
using HMSphere.MVC.ViewModels;
using HMSphere.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HMSphere.MVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IMapper _mapper;
        public AdminController(IAppointmentService appointmentService, IMapper mapper)
        {
            _appointmentService = appointmentService;
            _mapper = mapper;
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
        public async Task<IActionResult> PendingAppointments()
        {
            var pendingAppointments = await _appointmentService.GetPendingAppointments();

            var appointmentsViewModels = _mapper.Map<List<AppointmentViewModel>>(pendingAppointments);

            return View(appointmentsViewModels);
        }
        public IActionResult Patients()
        {
            return View();
        }

        public IActionResult Staff()
        {
            return View();
        }
        public IActionResult Doctors()
        {
            return View();
        }
    }
  
}


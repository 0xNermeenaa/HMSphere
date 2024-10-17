using AutoMapper;
using HMSphere.Application.DTOs;
using HMSphere.Application.Interfaces;
using HMSphere.Domain.Entities;
using HMSphere.Domain.Enums;
using HMSphere.Infrastructure.DataContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSphere.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly HmsContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        public AppointmentService(HmsContext context, 
                                  UserManager<ApplicationUser> userManager,
                                  IHttpContextAccessor httpContextAccessor,
                                  IMapper mapper
                                   )
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<List<Appointment>> GetAllAppointmentsAsync()
        {
            return new List<Appointment> { new Appointment() };
        }

        public async Task<AppointmentDto> CreateAppointment(AppointmentDto appointmentDto)
        {
            if (appointmentDto.Date == null || appointmentDto.AppointmentTime == null)
            {
                return new AppointmentDto
                {
                    IsSuccessful = false,
                    ErrorMessage = "Invalid date or time for the appointment."
                };
            }
            
            var doctor = await _context.Doctors.FindAsync(appointmentDto.DoctorId);
            if (doctor == null)
            {
                return new AppointmentDto
                {
                    IsSuccessful = false,
                    ErrorMessage = "The selected doctor does not exist."
                };  
            }
            var department = await _context.Departments.FindAsync(appointmentDto.DepartmentId);
            if (department == null)
            {
                return new AppointmentDto
                {
                    IsSuccessful = false,
                    ErrorMessage = "The selected department is invalid."
                };
            }

            var conflictingAppointment = await _context.Appointments
                .AnyAsync(a => a.DoctorId == appointmentDto.DoctorId
                               && a.Date == appointmentDto.Date
                               && a.AppointmentTime == appointmentDto.AppointmentTime);

            if (conflictingAppointment)
            {
                return new AppointmentDto
                {
                    IsSuccessful = false,
                    ErrorMessage = "The selected doctor is not available at this time."
                };
            }

            var appointment = _mapper.Map<Appointment>(appointmentDto);
            appointment.PatientId = await GetCurrentUserAsync();
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            appointmentDto.Status = Status.Scheduled;
            appointmentDto.IsApproved = null;
            appointmentDto.Id = appointment.Id;
            appointmentDto.PatientId = await GetCurrentUserAsync();
            appointmentDto.IsSuccessful = true;
            appointmentDto.ErrorMessage = null;
            return appointmentDto;
        }

        public async Task<bool> ApproveAppointment(int appointmentId, bool isApproved)
        {
            var appointment = await _context.Appointments.FindAsync(appointmentId);
            if (appointment == null)
            {
                return false; 
            }

            appointment.IsApproved = isApproved;
            appointment.Status = isApproved ? Status.Completed : Status.Cancelled;
            

            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<AppointmentDto>> GetScheduledAppointments()
        {
            var scheduledAppointments = await _context.Appointments
                .Where(a => a.Status == Status.Scheduled)
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .ToListAsync();
            if(scheduledAppointments==null) return new List<AppointmentDto>();

            return scheduledAppointments.Select(appointment => new AppointmentDto
            {
                Id = appointment.Id,
                Date = appointment.Date,
                Status = appointment.Status,
                IsApproved = appointment.IsApproved,
            }).ToList();
        }


        private async Task<string> GetCurrentUserAsync()
        {
            var userClaim = _httpContextAccessor.HttpContext!.User;

            // Get the current user from claims
            var user = await _userManager.GetUserAsync(userClaim);

            return user.Id;
        }



    }

}

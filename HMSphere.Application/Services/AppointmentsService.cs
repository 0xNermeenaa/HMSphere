using HMSphere.Application.Helpers;
using HMSphere.Application.Interfaces;
using HMSphere.Domain.Entities;
using HMSphere.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSphere.Application.Services
{
	public class AppointmentsService : IAppointmentService
	{
		private readonly HmsContext _context;
		private readonly IUserHelpers _userHelpers;

		public AppointmentsService(HmsContext context,IUserHelpers userHelpers)
		{
			_context = context;
			_userHelpers = userHelpers;

		}
		public async Task<List<Appointment>> GetAllAppointmentsAsync()
		{
			var currentUser = await _userHelpers.GetCurrentUserAsync() ?? throw new Exception("Current User Not found.");
			return await _context.Appointments.Include(d => d.Patient).ThenInclude(p=>p.User)
				.Where(a=> a.DoctorId== currentUser.Id).ToListAsync();
		}
	}
}

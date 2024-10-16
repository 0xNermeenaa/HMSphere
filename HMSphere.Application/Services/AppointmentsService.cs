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

		public AppointmentsService(HmsContext context)
		{
			_context = context;

		}
		public async Task<List<Appointment>> GetAllAppointmentsAsync()
		{
			return new List<Appointment> { new Appointment() };
		}
	}
}

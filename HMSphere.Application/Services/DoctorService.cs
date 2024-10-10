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
    public class DoctorService : IDoctorService
    {
		private readonly HmsContext _context;
		//private readonly DbSet<Patient> _dbSet;
		public DoctorService(HmsContext context)
		{
			_context = context;
			//_dbSet = _context.Set<Patient>();
		}

		public async Task<IEnumerable<Patient>> GetAllPatientAsync(string doctorId)
		{
			return await _context.MedicalRecords
				.Where(m => m.DoctorId == doctorId && !m.IsDeleted)
				.Select(m => m.Patient)
				.Distinct()
				.ToListAsync();
		}

	}
}

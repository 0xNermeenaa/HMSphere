using HMSphere.Application.Interfaces;
using HMSphere.Domain.Entities;
using HMSphere.Infrastructure.DataContext;
using HMSphere.Infrastructure.Repositories;
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
        private readonly IBaseRepository<MedicalRecord> _medicalRecord;

        //private readonly DbSet<Patient> _dbSet;
        public DoctorService(HmsContext context , IBaseRepository<MedicalRecord> medicalRecord)
		{
			_context = context;
			_medicalRecord = medicalRecord;
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
		public async Task<IEnumerable<MedicalRecord>> GetAllMedicalRecordsAsync(string doctorId,string patientId )
		{
			return await _context.MedicalRecords
				.Where(di => di.DoctorId == doctorId && di.PatientId == patientId)
				.ToListAsync();
		}

	}
}

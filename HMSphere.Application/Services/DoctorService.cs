using AutoMapper;
using HMSphere.Application.DTOs;
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
		private readonly IMapper _mapper;
		//private readonly IBaseRepository<MedicalRecord> _medicalRecord;
		//private readonly DbSet<Patient> _dbSet;
		public DoctorService(HmsContext context, IMapper mapper/* , IBaseRepository<MedicalRecord> medicalRecord*/)
		{
			_context = context;
			_mapper = mapper;
			//_medicalRecord = medicalRecord;
			//_dbSet = _context.Set<Patient>();
		}
		
		public async Task<IEnumerable<PatientDto>> GetAllPatientAsync(string doctorId)
		{
			var patients = await _context.MedicalRecords
				.Where(m => m.DoctorId == doctorId && !m.IsDeleted)
				.Select(m => m.Patient)
				.Distinct()
				.ToListAsync();
			var patientsResult = patients.Select(p => _mapper.Map<PatientDto>(p)).ToList();
			return patientsResult;
		}
		public async Task<IEnumerable<MedicalRecordDto>> GetAllMedicalRecordsAsync(string patientId)
		{
			var medicalRecords = await _context.MedicalRecords
				.Where(di => di.PatientId == patientId)
				.ToListAsync();
			var medicalRecordsResult = medicalRecords.Select(mr => _mapper.Map<MedicalRecordDto>(mr)).ToList();
			return medicalRecordsResult;
		}
		public async Task<bool> AddMedicalRecordAsync(MedicalRecordDto entity, string doctorId , string patientId)
		{ 
			return true;
		}

        public async Task<List<Doctor>> GetAllDoctorsAsync()
        {
			return  await _context.Doctors.ToListAsync();
			
        }
        public async Task<List<Doctor>> GetDoctorsByDepartmentIdAsync(int? departmentId)
        {
            if (departmentId == null)
            {
                return await _context.Doctors.Include(d => d.User).ToListAsync();
            }

            var doctors = await _context.Doctors
                .Where(d => d.DepartmentId == departmentId)
                .Include(d => d.User)
                .ToListAsync();

            return doctors;
        }





    }
}

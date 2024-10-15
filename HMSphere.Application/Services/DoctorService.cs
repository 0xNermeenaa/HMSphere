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

        public async Task<ResponseDTO> Profile(string id)
		{
			try
			{
				var doctor = await _context.Doctors.Include(d=>d.User)
					.Include(d=>d.Department)
					.FirstOrDefaultAsync(d=>d.Id==id);
				if (doctor != null)
				{
					return new ResponseDTO
					{
						IsSuccess = true,
						StatusCode = 200,
						Model = doctor
					};
				}

                return new ResponseDTO
                {
                    IsSuccess = false,
                    StatusCode = 404,
                    Message="Doctor not found!"
                };
            }
			catch (Exception ex)
			{
				return new ResponseDTO { IsSuccess = false, Message = "An error occured, please try again", StatusCode = 500 };
			}
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


	}
}

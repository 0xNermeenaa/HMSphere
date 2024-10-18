using HMSphere.Infrastructure.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMSphere.Application.DTOs;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using HMSphere.Domain.Entities;
using System.Reflection.Metadata.Ecma335;
using HMSphere.Application.Interfaces;

namespace HMSphere.Application.Services
{
    public class PatientService : IPatientService
    {
        private readonly HmsContext _context;
        private readonly IMapper _mapper;

        public PatientService(HmsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PatientDto>> GetAll()
        {
            try
            {
                var patients = await _context.Patients.Include(p => p.User)
                    .ToListAsync();
                if (!patients.Any())
                {
                    return new List<PatientDto>();
                }
                var dto = patients.Select(p => _mapper.Map<PatientDto>(p)).ToList();
                return dto;
            }
            catch
            {
                return new List<PatientDto>();
            }
        }

        public async Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync(string patientId)
        {
            var appointments = await _context.Appointments
               .Where(a => a.PatientId == patientId)
               .OrderByDescending(a => a.Date)
               .ToListAsync();
            return appointments.Select(a => _mapper.Map<AppointmentDto>(a)).ToList();
        }
        public async Task<IEnumerable<MedicalRecordDto>> GetAllMedicalRecordsAsync(string patientId)
        {
            var medicalrecords = await _context.MedicalRecords
              .Where(mr => mr.PatientId == patientId)
              .OrderByDescending(mr => mr.CreatedDate)
              .ToListAsync();
            return medicalrecords.Select(mr => _mapper.Map<MedicalRecordDto>(mr)).ToList();
        }
        public async Task<IEnumerable<AppointmentDto>> GetLast5AppointmentsAsync(string patientId)
        {
            var Latest5Appointments = await _context.Appointments
                                 .Where(a => a.PatientId == patientId)
                                 .OrderByDescending(a => a.Date)
                                 .Take(5)
                                 .ToListAsync();
            return Latest5Appointments.Select(a => _mapper.Map<AppointmentDto>(a)).ToList();
        }
        public async Task<IEnumerable<MedicalRecordDto>> GetLast5MedicalRecordsAsync(string patientId)
        {
            var Latest5MedicalRecords = await _context.MedicalRecords
                                 .Where(mr => mr.PatientId == patientId)
                                 .OrderByDescending(mr => mr.CreatedDate)
                                 .Take(5)
                                 .ToListAsync();
            return Latest5MedicalRecords.Select(mr => _mapper.Map<MedicalRecordDto>(mr)).ToList();
        }
    }
}

using HMSphere.Application.DTOs;
using HMSphere.Domain.Entities;
using HMSphere.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSphere.Application.Interfaces
{
    public interface IDoctorService
    {
		Task<IEnumerable<PatientDto>> GetAllPatientAsync(string doctorId);
        Task<IEnumerable<MedicalRecordDto>> GetAllMedicalRecordsAsync(string pateintId);
		Task<bool> AddMedicalRecordAsync(MedicalRecordDto entity, string doctorId , string patientId);
        Task<ResponseDTO> Profile(string id);
	}
}

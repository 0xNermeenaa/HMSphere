using HMSphere.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSphere.Application.Interfaces
{
    public interface IPatientService
    {
        Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync(string PatientID);
        Task<IEnumerable<MedicalRecordDto>> GetAllMedicalRecordsAsync(string PateintID);
        Task<IEnumerable<AppointmentDto>> GetLast5AppointmentsAsync(string PatientID);
        Task<IEnumerable<MedicalRecordDto>> GetLast5MedicalRecordsAsync(string PatientID);
        Task<IEnumerable<PatientDto>> GetAll();
    }
}

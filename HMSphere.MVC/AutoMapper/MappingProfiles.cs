using AutoMapper;
using HMSphere.Application.DTOs;
using HMSphere.Domain.Entities;
using HMSphere.MVC.ViewModels;

namespace HMSphere.MVC.AutoMapper
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<AuthDto,AuthViewModel>().ReverseMap();
            CreateMap<RegisterDto, RegisterViewModel>().ReverseMap();
            CreateMap<LoginDto, LoginViewModel>().ReverseMap();
            CreateMap<Patient,PatientDto>().ReverseMap();   
            CreateMap<PatientDto , PatientsHistoryViewModel>().ReverseMap();
			CreateMap<MedicalRecord, MedicalRecordDto>().ReverseMap();
			CreateMap<MedicalRecordDto , MedicalRecordViewModel>().ReverseMap();
        }
    }
}

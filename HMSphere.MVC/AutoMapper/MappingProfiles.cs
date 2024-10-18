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
            CreateMap<Patient,PatientDto>()
                .ForMember(dest=>dest.PatientId,p=>p.MapFrom(src=>src.Id))
                .ForMember(dest=>dest.FirstName,p=>p.MapFrom(src=>src.User.FirstName))
				.ForMember(dest=>dest.LastName,p=>p.MapFrom(src=>src.User.LastName))
                .ForMember(dest=>dest.Gender,p=>p.MapFrom(src=>src.User.Gender))
                .ForMember(dest=>dest.PhoneNumber,p=>p.MapFrom(src=>src.User.PhoneNumber))
				.ReverseMap();   
            CreateMap<Doctor , DoctorDto>()
                .ForMember(dest=>dest.FirstName,d=>d.MapFrom(d=>d.User.FirstName))
                .ForMember(dest=>dest.LastName,d=>d.MapFrom(d=>d.User.LastName))
                .ForMember(dest=>dest.PhoneNumber,d=>d.MapFrom(d=>d.User.PhoneNumber))
                .ForMember(dest=>dest.Department,d=>d.MapFrom(d=>d.Department.Name))
				.ReverseMap();
            CreateMap<DoctorDto , DoctorViewModel>().ReverseMap();
            CreateMap<PatientDto , PatientsHistoryViewModel>().ReverseMap();
            CreateMap<Staff, StaffDto>()
                .ForMember(dest=>dest.FirstName,s=>s.MapFrom(src=>src.User.FirstName))
                .ForMember(dest=>dest.LastName,s=>s.MapFrom(src=>src.User.LastName))
                .ForMember(dest=>dest.PhoneNumber,s=>s.MapFrom(src=>src.User.PhoneNumber))
                .ForMember(dest=>dest.Department,s=>s.MapFrom(src=>src.Department.Name))
				.ReverseMap();
            CreateMap<MedicalRecord, MedicalRecordDto>().ReverseMap();
			CreateMap<MedicalRecordDto , MedicalRecordViewModel>().ReverseMap();
			CreateMap<Doctor , DoctorViewModel>()
                .ForMember(dest=>dest.FirstName,o=>o.MapFrom(src=>src.User.FirstName))
                .ForMember(dest => dest.LastName, o => o.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.Specialization, o => o.MapFrom(src => src.Specialization))
                .ForMember(dest => dest.Department, o => o.MapFrom(src => src.Department.Name))
                .ReverseMap();
            CreateMap<Appointment, AppointmentDto>()
                .ForMember(dest=>dest.PatientName,a=>a.MapFrom(src=>src.Patient.User.FirstName))
                .ReverseMap();
            CreateMap<AppointmentDto, AppointmentsViewModel>().ReverseMap();
            CreateMap<AppointmentDto, AppointmentViewModel>().ReverseMap();
            CreateMap<MedicalRecordDto, PatientMedicalRecordsViewModel>().ReverseMap();
            CreateMap<AppointmentDto, PatientAppointmentsViewModel>().ReverseMap();
        }
    }
}

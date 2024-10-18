using HMSphere.Application.DTOs;
using HMSphere.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSphere.Application.Interfaces
{
	public interface IAppointmentService
	{
        Task<AppointmentDto> CreateAppointment(AppointmentDto appointmentDto);
        Task<bool> ApproveAppointment(int appointmentId, bool isApproved);
        Task<IEnumerable<AppointmentDto>> GetPendingAppointments();
	}
}

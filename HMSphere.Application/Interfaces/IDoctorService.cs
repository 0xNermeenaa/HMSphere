using HMSphere.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSphere.Application.Interfaces
{
    public interface IDoctorService
    {
		Task<IEnumerable<Patient>> GetAllPatientAsync(string doctorId);
	}
}

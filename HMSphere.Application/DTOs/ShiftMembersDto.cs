using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSphere.Application.DTOs
{
    public class ShiftMembersDto
    {
		public List<DoctorDto> Doctors { get; set; } = new List<DoctorDto>();
        public List<StaffDto> Staff { get; set; } = new List<StaffDto>();
    }
}

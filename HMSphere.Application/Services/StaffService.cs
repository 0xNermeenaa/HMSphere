using AutoMapper;
using HMSphere.Application.DTOs;
using HMSphere.Application.Interfaces;
using HMSphere.Domain.Entities;
using HMSphere.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSphere.Application.Services
{
    public class StaffService : IStaffService
	{
		private readonly HmsContext _context;
		private readonly IMapper _mapper;
        private readonly DbSet<StaffShift> _dbSet;
		private readonly DbSet<DoctorShift> _dbSet2;


		public StaffService(HmsContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
            _dbSet = _context.Set<StaffShift>();
			_dbSet2 = _context.Set<DoctorShift>();

        }

		public async Task<StaffDto> GetById(string id)
		{
			var staff = await _context.Staff.Include(s => s.User)
				.FirstOrDefaultAsync(s => s.Id == id);

			if (staff != null)
			{
				var dto=_mapper.Map<StaffDto>(staff);
				return dto;
			}
			return new StaffDto();
		}

		public async Task<IEnumerable<ShiftDto>> GetShiftsForStaffAsync(string StaffId)
		{
            var StaffShifts = await _context.StaffShifts
                .Where(m => m.StaffId == StaffId && !m.IsDeleted)
                .Select(m => m.Shift)
                .Distinct()
                .ToListAsync();
            var ShiftsResult = StaffShifts.Select(p => _mapper.Map<ShiftDto>(p)).ToList();
            return ShiftsResult;
		}
		public async Task<IEnumerable<StaffDto>> GetAllAsync()
		{
			var staff = await _context.Staff.Include(d => d.User)
					.Include(d => d.Department).ToListAsync();
			if (!staff.Any())
			{
				return new List<StaffDto>();
			}
			var dto = staff.Select(s => _mapper.Map<StaffDto>(s)).ToList();
			return dto;
		}


		public async Task<bool> AssignStaffToShiftAsync(int shiftId , string staffId)
        {
            var StaffShift = new StaffShift { ShiftId = shiftId, StaffId = staffId};
            await _dbSet.AddAsync(StaffShift);
            return await _context.SaveChangesAsync() >0;
        }
		public async Task<bool> AssignDoctorToShiftAsync(int shiftId, string doctorId)
		{
			var DoctorShift = new DoctorShift { ShiftId = shiftId, DoctorId = doctorId };
			await _dbSet2.AddAsync(DoctorShift);
			return await _context.SaveChangesAsync() > 0;
		}
	}
}

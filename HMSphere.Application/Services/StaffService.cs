using AutoMapper;
using HMSphere.Application.DTOs;
using HMSphere.Application.Interfaces;
using HMSphere.Domain.Entities;
using HMSphere.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
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

		public StaffService(HmsContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
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
	}
}

using AutoMapper;
using HMSphere.Application.DTOs;
using HMSphere.Application.Interfaces;
using HMSphere.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSphere.Application.Services
{
	public class StaffService:IStaffService
	{
		private readonly HmsContext _context;
		private readonly IMapper _mapper;

		public StaffService(HmsContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<List<StaffDto>> GetAllAsync()
		{
			var staff=await _context.Staff.Include(s=>s.User)
				.Include(s=>s.Department).ToListAsync();

			if (!staff.Any())
			{
				return new List<StaffDto>();
			}

			var dto = staff.Select(s => _mapper.Map<StaffDto>(s)).ToList();
			return dto;
		}
	}
}

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
    public class ShiftService : IShiftService
    {
        private readonly HmsContext _context;
        private readonly DbSet<StaffShift> _dbSet;
        private readonly DbSet<DoctorShift> _dbSet2;
        public ShiftService(HmsContext context)
        {
            _context = context;
            _dbSet = _context.Set<StaffShift>();
            _dbSet2 = _context.Set<DoctorShift>();

        }
        public async Task<bool> AssignStaffToShiftAsync(int shiftId, string staffId)
        {
            bool isAlreadyAssigned = await _dbSet.AnyAsync(ds => ds.ShiftId == shiftId && ds.StaffId == staffId);

            if (isAlreadyAssigned)
            {
                return false;
            }
            var StaffShift = new StaffShift { ShiftId = shiftId, StaffId = staffId };
            await _dbSet.AddAsync(StaffShift);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> AssignDoctorToShiftAsync(int shiftId, string doctorId)
        {
            bool isAlreadyAssigned = await _dbSet2.AnyAsync(ds => ds.ShiftId == shiftId && ds.DoctorId == doctorId);

            if (isAlreadyAssigned)
            {
                return false;
            }
            var DoctorShift = new DoctorShift { ShiftId = shiftId, DoctorId = doctorId };
            await _dbSet2.AddAsync(DoctorShift);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<ShiftMembersDto> GetShiftMembersAsync(int shiftId)
        {
            var doctors = await _context.DoctorShifts
                .Where(ds => ds.ShiftId == shiftId)
                .Include(ds => ds.Doctor)
                .Select(ds => new DoctorDto
                {
                    Id = ds.Doctor.Id,
                    FirstName = ds.Doctor.User.FirstName,
                    LastName = ds.Doctor.User.LastName,
                    Specialization = ds.Doctor.Specialization
                })
                .ToListAsync();

            var staff = await _context.StaffShifts
                .Where(ss => ss.ShiftId == shiftId)
                .Include(ss => ss.Staff)
                .Select(ss => new StaffDto
                {
                    Id = ss.Staff.Id,
                    FirstName = ss.Staff.User.FirstName,
                    LastName = ss.Staff.User.LastName,
                    JobTitle = ss.Staff.JobTitle
                })
                .ToListAsync();

            return new ShiftMembersDto
            {
                Doctors = doctors,
                Staff = staff
            };
        }
    }
}

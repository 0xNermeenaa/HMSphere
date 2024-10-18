using HMSphere.Application.DTOs;

namespace HMSphere.MVC.ViewModels
{
    public class ShiftManagementViewModel
    {
        public List<ShiftViewModel> Shifts { get; set; } = new List<ShiftViewModel>();
        public ShiftDto NewShift { get; set; } = new ShiftDto();
        public List<StaffViewModel> Staff { get; set; } = new List<StaffViewModel>();
    }
}

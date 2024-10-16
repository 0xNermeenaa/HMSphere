using HMSphere.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HMSphere.MVC.ViewModels
{
    public class AppointmentViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public Status Status { get; set; }
        [Required]
        public string ReasonFor { get; set; }
        [Required]
        public string Clinic { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; } 
    }
}

using System.ComponentModel.DataAnnotations;

namespace HMSphere.MVC.ViewModels
{
    public class MedicalRecordViewModel
    {
        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public string Diagnosis { get; set; }

        [Required]
        public string? TreatmentPlan { get; set; }

        [Required]
        public string Medications { get; set; }

        [Required]
        public string? DoctorNotes { get; set; }
        [Required]
        public DateTime LastUpdated { get; set; }
    }
}

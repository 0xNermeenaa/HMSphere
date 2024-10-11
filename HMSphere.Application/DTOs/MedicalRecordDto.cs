namespace HMSphere.Application.DTOs
{
    public class MedicalRecordDto
    {
        public DateTime CreatedDate { get; set; }
        public string Diagnosis { get; set; }
        public string? TreatmentPlan { get; set; }
        public string Medications { get; set; }
        public string? DoctorNotes { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}

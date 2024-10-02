using HMSphere.Domain.Enums;


namespace HMSphere.Domain.Entities
{
    public class Staff : ApplicationUser
    {

        public Role Role { get; set; }

        //[Required]
        //[StringLength(100)]
        public string JobTitle { get; set; }


        //[Required]
        //[DataType(DataType.Date)]
        public DateTime HireDate { get; set; }

        // Foreign Key for DeptId
        //[Required]
        public int DeptId { get; set; }


        // Foreign Key for Shift 
        //[Required]
        public int ShiftId { get; set; }
    }
}

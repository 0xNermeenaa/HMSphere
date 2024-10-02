﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HMSphere.Domain.Entities
{
    public class Department
    {
        public int ID { get; set; }
        [Required,MaxLength(30)]
        public string Name { get; set; }
        [Required,MaxLength(200)]
        public string Description { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        [Required,MaxLength(30)]
        public string Location { get; set; }
        public int ManagerID { get; set; }
        [ForeignKey("ManagerID")]
        public Doctor DeptManager { get; set; }
        public ICollection<Doctor> doctors { get; set; }
        public ICollection<Staff> Staff { get; set; }
    }
}

using E_Commerce.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Text.Json.Serialization;

namespace HMSphere.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NID { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public bool IsDeleted { get; set; } = false;
        [JsonIgnore]
        public List<RefreshToken>? RefreshTokens { get; set; }
    }
}

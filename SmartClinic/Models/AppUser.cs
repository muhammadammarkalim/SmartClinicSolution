using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace SmartClinic.Models
{
    /// <summary>
    /// Represents the custom user entity extending IdentityUser to include role-based access.
    /// </summary>
    public class AppUser : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Role { get; set; } = string.Empty; // Patient, Doctor, Admin

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
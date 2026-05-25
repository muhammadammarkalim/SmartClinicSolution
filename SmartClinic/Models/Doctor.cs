using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartClinic.Models
{
    /// <summary>
    /// Holds profile, qualification, and professional details for a Doctor.
    /// </summary>
    public class Doctor
    {
        [Key]
        public int DoctorID { get; set; }

        [Required]
        public string UserID { get; set; } = string.Empty;

        [ForeignKey("UserID")]
        public virtual AppUser? User { get; set; }

        [Required]
        [StringLength(50)]
        public string Specialty { get; set; } = string.Empty;

        [Required]
        [StringLength(150)]
        public string Qualification { get; set; } = string.Empty;

        [Required]
        [StringLength(1000)]
        public string Bio { get; set; } = string.Empty;

        [Range(0.0, 5.0)]
        public double Rating { get; set; } = 0.0;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal ConsultationFee { get; set; }

        [StringLength(255)]
        public string? Photo { get; set; }

        public bool IsAvailable { get; set; } = true;

        // Navigation properties
        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
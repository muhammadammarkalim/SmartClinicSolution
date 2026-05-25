using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartClinic.Models
{
    /// <summary>
    /// Represents patient-submitted feedback and star ratings for a specific doctor.
    /// </summary>
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }

        [Required]
        public int DoctorID { get; set; }

        [ForeignKey("DoctorID")]
        public virtual Doctor? Doctor { get; set; }

        [Required]
        public string PatientID { get; set; } = string.Empty;

        [ForeignKey("PatientID")]
        public virtual AppUser? Patient { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [StringLength(1000)]
        public string? Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
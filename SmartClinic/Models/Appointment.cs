using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartClinic.Models
{
    /// <summary>
    /// Represents a clinic appointment booking between a Patient and a Doctor.
    /// </summary>
    public class Appointment
    {
        [Key]
        public int AppointmentID { get; set; }

        [Required]
        public string PatientID { get; set; } = string.Empty;

        [ForeignKey("PatientID")]
        public virtual AppUser? Patient { get; set; }

        [Required]
        public int DoctorID { get; set; }

        [ForeignKey("DoctorID")]
        public virtual Doctor? Doctor { get; set; }

        [Required]
        public DateTime AppointmentDateTime { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Booked"; // Booked, Confirmed, In Progress, Completed, Cancelled

        [Required]
        [StringLength(20)]
        public string Urgency { get; set; } = "Routine"; // Routine, Moderate, Urgent

        [StringLength(500)]
        public string? PatientNotes { get; set; }

        public virtual Prescription? Prescription { get; set; }
    }
}
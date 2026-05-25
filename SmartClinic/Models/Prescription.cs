using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartClinic.Models
{
    /// <summary>
    /// Holds medical diagnostics and references to prescribed treatments for an appointment.
    /// </summary>
    public class Prescription
    {
        [Key]
        public int PrescriptionID { get; set; }

        [Required]
        public int AppointmentID { get; set; }

        [ForeignKey("AppointmentID")]
        public virtual Appointment? Appointment { get; set; }

        [Required]
        public int DoctorID { get; set; }

        [ForeignKey("DoctorID")]
        public virtual Doctor? Doctor { get; set; }

        [Required]
        public string PatientID { get; set; } = string.Empty;

        [ForeignKey("PatientID")]
        public virtual AppUser? Patient { get; set; }

        [Required]
        [StringLength(250)]
        public string Diagnosis { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? DoctorNotes { get; set; }

        public DateTime IssuedDate { get; set; } = DateTime.Now;

        public virtual ICollection<PrescriptionMedicine> Medicines { get; set; } = new List<PrescriptionMedicine>();
    }
}
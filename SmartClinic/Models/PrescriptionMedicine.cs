using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartClinic.Models
{
    /// <summary>
    /// Represents an individual itemized medicine within a master Prescription.
    /// </summary>
    public class PrescriptionMedicine
    {
        [Key]
        public int MedID { get; set; }

        [Required]
        public int PrescriptionID { get; set; }

        [ForeignKey("PrescriptionID")]
        public virtual Prescription? Prescription { get; set; }

        [Required]
        [StringLength(100)]
        public string MedicineName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Dosage { get; set; } = string.Empty; // e.g., 500mg, 5ml

        [Required]
        [StringLength(50)]
        public string Frequency { get; set; } = string.Empty; // e.g., Once daily, Twice daily

        [Required]
        [StringLength(50)]
        public string Duration { get; set; } = string.Empty; // e.g., 7 days, 1 month

        [StringLength(500)]
        public string? Instructions { get; set; } // e.g., Take after meals
    }
}
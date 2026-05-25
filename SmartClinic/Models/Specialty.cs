using System.ComponentModel.DataAnnotations;

namespace SmartClinic.Models
{
    /// <summary>
    /// Represents a medical specialty department available within the clinic system.
    /// </summary>
    public class Specialty
    {
        [Key]
        public int SpecialtyID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Icon { get; set; } = string.Empty; // Holds layout emojis or CSS class icons
    }
}



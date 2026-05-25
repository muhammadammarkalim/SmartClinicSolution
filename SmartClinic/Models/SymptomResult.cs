using System.Collections.Generic;

namespace SmartClinic.Models
{
    /// <summary>
    /// Lightweight transient container to hold processed Gemini API inferences and mapped database doctors.
    /// This is NOT a database table.
    /// </summary>
    public class SymptomResult
    {
        public string PossibleCause { get; set; } = string.Empty;
        public string RecommendedSpecialty { get; set; } = string.Empty;
        public string UrgencyLevel { get; set; } = string.Empty; // Routine, Moderate, Urgent
        public string Advice { get; set; } = string.Empty;
        public string WarningMessage { get; set; } = string.Empty;

        // Holds top matching doctors queried directly from our local SQL tables
        public List<Doctor> MatchedDoctors { get; set; } = new();
    }
}
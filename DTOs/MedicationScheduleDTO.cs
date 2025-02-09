using System.ComponentModel.DataAnnotations;

namespace MyMedCalendar.DTOs
{
    public class MedicationScheduleDTO : IValidatableObject
    {
        public int Id { get; set; }
        public int DrugId { get; set; }
        public string PatientId { get; set; } = string.Empty; 
        public string DrugName { get; set; } = string.Empty; // Optional: for display purposes
        public int Dosage { get; set; }
        public int FrequencyPerWeek { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndDate < StartDate)
            {
                yield return new ValidationResult(
                    "End Date cannot be earlier than Start Date.",
                    new[] { nameof(EndDate), nameof(StartDate) });
            }
        }
    }
}

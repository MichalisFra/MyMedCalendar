using System.ComponentModel.DataAnnotations;

namespace MyMedCalendar.DTOs
{
    public class MedicationEventDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int MedicationScheduleId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        public string? Notes { get; set; } // Existing Notes property for event details
    }
}

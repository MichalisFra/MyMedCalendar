using System.ComponentModel.DataAnnotations;

namespace MyMedCalendar.DTOs
{
    public class MedicationEventReadOnlyDTO
    {
        
        public int Id { get; set; }

        
        public int MedicationScheduleId { get; set; }

       
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        public string? Notes { get; set; } // Existing Notes property for event details
    }
}

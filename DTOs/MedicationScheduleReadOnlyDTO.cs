namespace MyMedCalendar.DTOs
{
    public class MedicationScheduleReadOnlyDTO
    {
        public int Id { get; set; }
        public string Dosage { get; set; } = string.Empty;
        public int FrequencyPerWeek { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public PatientReadOnlyDTO Patient { get; set; } = null!;
        public DrugReadOnlyDTO Drug { get; set; } = null!;
    }
}
namespace MyMedCalendar.Models
{
    public class Drug : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Company { get; set; } = null!;
        public int? PackageSize { get; set; }

        // Navigation Properties
        public virtual ICollection<MedicationSchedule>? MedicationSchedules { get; set; }
    }
}

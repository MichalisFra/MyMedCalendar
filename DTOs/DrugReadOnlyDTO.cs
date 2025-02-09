namespace MyMedCalendar.DTOs
{
    public class DrugReadOnlyDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Company { get; set; }
        public int? PackageSize { get; set; }
    }
}

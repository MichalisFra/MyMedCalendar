namespace MyMedCalendar.DTOs
{
    public class PatientReadOnlyDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string AMKA { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
    }
}

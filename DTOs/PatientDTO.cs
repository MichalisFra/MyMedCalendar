using System.ComponentModel.DataAnnotations;

namespace MyMedCalendar.DTOs
{
    public class PatientDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        [RegularExpression(@"\d{11}", ErrorMessage = "AMKA must be 11 digits.")]
        public string AMKA { get; set; } = null!;

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;

namespace MyMedCalendar.Models
{
    public class Patient : BaseEntity
    {


        public string Id { get; set; } = null!;

        public string UserId { get; set; } = null!; 
        public virtual ApplicationUser User { get; set; } = null!; // Navigation property

        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string AMKA { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }


        public virtual ICollection<MedicationSchedule>? MedicationSchedules { get; set; }
    }
}

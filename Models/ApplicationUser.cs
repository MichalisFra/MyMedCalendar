using Microsoft.AspNetCore.Identity;

namespace MyMedCalendar.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime InsertedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}


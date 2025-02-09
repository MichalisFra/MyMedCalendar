namespace MyMedCalendar.DTOs
{
    public class UserReadOnlyDTO
    {
        /// <summary>
        /// Unique identifier of the user.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// The user's email address.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// The user's username.
        /// </summary>
        public string? Username { get; set; }
    }
}

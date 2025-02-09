namespace MyMedCalendar.DTOs
{
    public class UserDTO
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

        /// <summary>
        /// The user's password. Only used during creation or updates.
        /// </summary>
        public string? Password { get; set; }
    }
}

using MyMedCalendar.DTOs;

namespace MyMedCalendar.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Registers a new user and creates a corresponding Patient entity.
        /// </summary>
        /// <param name="signupDto">The DTO containing user registration details.</param>
        Task SignUpUserAsync(SignupDTO signupDto);

        /// <summary>
        /// Retrieves the ID of the currently logged-in user.
        /// </summary>
        /// <returns>The ID of the currently logged-in user.</returns>
        Task<string> GetCurrentUserIdAsync();

        /// <summary>
        /// Logs in a user with the provided email/username and password.
        /// </summary>
        /// <param name="emailOrUsername">The email or username of the user.</param>
        /// <param name="password">The user's password.</param>
        Task SignInAsync(string emailOrUsername, string password);

        /// <summary>
        /// Logs out the currently logged-in user.
        /// </summary>
        Task SignOutAsync();

        /// <summary>
        /// Updates the email of an existing user.
        /// </summary>
        /// <param name="userId">The ID of the user whose email is to be updated.</param>
        /// <param name="newEmail">The new email address for the user.</param>
        Task UpdateEmailAsync(string userId, string newEmail);
    }
}

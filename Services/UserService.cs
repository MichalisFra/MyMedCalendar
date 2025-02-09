using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using MyMedCalendar.DTOs;
using MyMedCalendar.Models;
using MyMedCalendar.Repositories;
using MyMedCalendar.Exceptions;

namespace MyMedCalendar.Services
{
    /// <summary>
    /// Manages user-related operations such as authentication, registration, and user profile updates,
    /// leveraging ASP.NET Identity for user management.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;


        /// <summary>
        /// Constructs the UserService with necessary dependencies.
        /// </summary>
        /// <param name="userManager">Manages user persistence and authentication.</param>
        /// <param name="signInManager">Manages user sign-in processes.</param>
        /// <param name="httpContextAccessor">Accesses the HTTP context to manage user sessions.</param>
        /// <param name="patientRepository">Interacts with patient data in the repository.</param>
        /// <param name="mapper">Maps data between DTOs and business models.</param>
        /// <param name="unitOfWork">Coordinates transactions across repositories.</param>
        public UserService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IHttpContextAccessor httpContextAccessor,
            IPatientRepository patientRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _patientRepository = patientRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        /// <summary>
        /// Registers a new user and optionally creates a patient record.
        /// </summary>
        /// <param name="signupDto">The user's signup information.</param>
        public async Task SignUpUserAsync(SignupDTO signupDto)
        {
            // Create the User
            var user = new ApplicationUser
            {
                UserName = signupDto.Username,
                Email = signupDto.Email,
                InsertedAt = DateTime.UtcNow, // Explicitly set InsertedAt
                ModifiedAt = DateTime.UtcNow  // Explicitly set ModifiedAt
            };

            var result = await _userManager.CreateAsync(user, signupDto.Password!);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException("User creation failed: " +
                    string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            // Create the Patient entity with the same Id as the User
            var patient = new Patient
            {
                Id = user.Id,
                UserId = user.Id,
                FirstName = string.Empty,
                LastName = string.Empty,
                AMKA = string.Empty,
                DateOfBirth = DateTime.MinValue,
                InsertedAt = user.InsertedAt, // Match User's InsertedAt
                ModifiedAt = user.ModifiedAt  // Match User's ModifiedAt
            };

            await _unitOfWork.PatientRepository.AddAsync(patient);
            await _unitOfWork.SaveAsync();

            // Automatically log in the user after successful registration
            await _signInManager.SignInAsync(user, isPersistent: false);
        }


        /// <summary>
        /// Retrieves the currently logged-in user's ID.
        /// </summary>
        /// <returns>The user's ID if logged in; throws an exception otherwise.</returns>
        public async Task<string> GetCurrentUserIdAsync()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User!);
            if (user == null || string.IsNullOrWhiteSpace(user.Id))
            {
                throw new InvalidOperationException("No user is currently logged in.");
            }

            return user.Id; // IdentityUser.Id is a string
        }

        /// <summary>
        /// Attempts to log in a user with the specified credentials.
        /// </summary>
        /// <param name="emailOrUsername">Email or username for login.</param>
        /// <param name="password">Password for login.</param>
        public async Task SignInAsync(string emailOrUsername, string password)
        {
            var user = await _userManager.FindByEmailAsync(emailOrUsername);

            if (user == null)
            {
                user = await _userManager.FindByNameAsync(emailOrUsername);
            }

            if (user == null)
            {
                Console.WriteLine("User not found.");
                throw new EntityNotAuthorizedException("","Invalid credentials.");
            }

            Console.WriteLine($"Attempting login for user: {user.UserName}");

            var result = await _signInManager.PasswordSignInAsync(user.UserName!, password, false, false);

            Console.WriteLine($"Login result: {result.Succeeded}, IsLockedOut: {result.IsLockedOut}, IsNotAllowed: {result.IsNotAllowed}");

            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                {
                    throw new EntityNotAuthorizedException("", "Account is locked out.");
                }
                else if (result.IsNotAllowed)
                {
                    throw new EntityNotAuthorizedException("", "Login is not allowed. Email might not be confirmed.");
                }
                else
                {
                    throw new EntityNotAuthorizedException("", "Invalid credentials.");
                }
            }
        }

        /// <summary>
        /// Logs out the current user.
        /// </summary>
        public async Task SignOutAsync() => await _signInManager.SignOutAsync();

        /// <summary>
        /// Updates the email of an existing user.
        /// </summary>
        public async Task UpdateEmailAsync(string userId, string newEmail)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new EntityNotFoundException("", "User not found.");
            }

            var token = await _userManager.GenerateChangeEmailTokenAsync(user, newEmail);
            var result = await _userManager.ChangeEmailAsync(user, newEmail, token);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Failed to change email.");
            }
        }
    }
}

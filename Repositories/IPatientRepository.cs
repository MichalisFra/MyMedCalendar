using MyMedCalendar.Models;

/// <summary>
/// Defines an interface for a repository that handles operations specific to <see cref="Patient"/> entities,
/// extending the basic CRUD operations with additional functionality to retrieve patient-specific data.
/// </summary>
namespace MyMedCalendar.Repositories
{
    public interface IPatientRepository : IBaseRepository<Patient>
    {
        /// <summary>
        /// Asynchronously retrieves a patient by their associated user ID, including detailed information such as medication schedules.
        /// </summary>
        /// <param name="userId">The user ID associated with the patient.</param>
        /// <returns>The patient if found; otherwise, null.</returns>
        Task<Patient?> GetByUserIdAsync(string userId);

        /// <summary>
        /// Asynchronously retrieves all patients, including their medication schedules.
        /// </summary>
        /// <returns>A collection of all patients along with their associated medication schedules.</returns>
        Task<IEnumerable<Patient>> GetPatientsWithSchedulesAsync();

        /// <summary>
        /// Asynchronously retrieves a single patient by their ID, including their medication schedules and the related drug information.
        /// </summary>
        /// <param name="patientId">The ID of the patient to retrieve.</param>
        /// <returns>The patient with their schedules and related drug details if found; otherwise, null.</returns>
        Task<Patient?> GetPatientWithSchedulesAsync(string patientId);
    }
}

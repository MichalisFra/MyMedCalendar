using MyMedCalendar.DTOs;

namespace MyMedCalendar.Services
{
    /// <summary>
    /// Defines a contract for services that manage medication-related operations, providing methods
    /// for handling medication schedules and generating related calendar events.
    /// </summary>
    public interface IMedicationService
    {
        /// <summary>
        /// Retrieves all medication schedules for a specific patient by their patient ID.
        /// </summary>
        /// <param name="patientId">The ID of the patient whose schedules are to be retrieved.</param>
        /// <returns>A collection of medication schedule DTOs for the specified patient.</returns>
        Task<IEnumerable<MedicationScheduleDTO>> GetSchedulesByPatientIdAsync(string patientId);

        /// <summary>
        /// Retrieves a specific medication schedule by its ID.
        /// </summary>
        /// <param name="scheduleId">The ID of the medication schedule to retrieve.</param>
        /// <returns>The medication schedule DTO if found; otherwise, null.</returns>
        Task<MedicationScheduleDTO?> GetScheduleByIdAsync(int scheduleId);

        /// <summary>
        /// Retrieves all medication schedules associated with a specific user by their user ID.
        /// </summary>
        /// <param name="userId">The ID of the user whose schedules are to be retrieved.</param>
        /// <returns>A collection of medication schedule DTOs associated with the specified user.</returns>
        Task<IEnumerable<MedicationScheduleDTO>> GetSchedulesByUserIdAsync(string userId);

        /// <summary>
        /// Adds a new medication schedule based on the provided DTO and associates it with a user by their user ID.
        /// </summary>
        /// <param name="scheduleDto">The medication schedule DTO to add.</param>
        /// <param name="userId">The user ID to associate with the medication schedule.</param>
        Task AddScheduleAsync(MedicationScheduleDTO scheduleDto, string userId);

        /// <summary>
        /// Updates an existing medication schedule using the provided DTO.
        /// </summary>
        /// <param name="scheduleDto">The medication schedule DTO containing the updated details.</param>
        Task UpdateScheduleAsync(MedicationScheduleDTO scheduleDto);

        /// <summary>
        /// Deletes a medication schedule by its ID.
        /// </summary>
        /// <param name="scheduleId">The ID of the medication schedule to delete.</param>
        Task DeleteScheduleAsync(int scheduleId);

        /// <summary>
        /// Generates a list of calendar events based on a set of medication schedule IDs.
        /// </summary>
        /// <param name="scheduleIds">An array of medication schedule IDs for which to generate calendar events.</param>
        /// <returns>A list of medication event DTOs representing calendar events.</returns>
        Task<List<MedicationEventDTO>> GenerateCalendarEventsAsync(int[] scheduleIds);
    }
}

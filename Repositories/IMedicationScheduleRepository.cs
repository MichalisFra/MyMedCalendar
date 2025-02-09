using MyMedCalendar.Models;

/// <summary>
/// Defines an interface for a repository that manages operations specific to <see cref="MedicationSchedule"/> entities.
/// </summary>
namespace MyMedCalendar.Repositories
{
    public interface IMedicationScheduleRepository
    {
        /// <summary>
        /// Asynchronously retrieves all medication schedules associated with a specific patient by the patient's ID.
        /// </summary>
        /// <param name="patientId">The ID of the patient whose medication schedules are to be retrieved.</param>
        /// <returns>A collection of medication schedules associated with the specified patient.</returns>
        Task<IEnumerable<MedicationSchedule>> GetByPatientIdAsync(string patientId);

        /// <summary>
        /// Asynchronously retrieves medication schedules by a list of schedule IDs.
        /// </summary>
        /// <param name="ids">A collection of IDs for the medication schedules to retrieve.</param>
        /// <returns>A collection of medication schedules that match the provided IDs.</returns>
        Task<IEnumerable<MedicationSchedule>> GetByIdsAsync(IEnumerable<int> ids);
    }
}

using MyMedCalendar.DTOs;

namespace MyMedCalendar.Services
{
    /// <summary>
    /// Defines a contract for services that manage patient-related operations, offering methods
    /// to retrieve and update patient information, as well as to fetch patient data including their medication schedules.
    /// </summary>
    public interface IPatientService
    {
        /// <summary>
        /// Retrieves patient information by user ID and maps it to a PatientDTO.
        /// </summary>
        /// <param name="userId">The user ID associated with the patient.</param>
        /// <returns>The patient DTO if found; otherwise, null.</returns>
        Task<PatientDTO?> GetPatientByUserIdAsync(string userId);

        /// <summary>
        /// Updates the patient information based on the provided PatientDTO.
        /// </summary>
        /// <param name="userId">The user ID of the patient whose information is to be updated.</param>
        /// <param name="patientDto">The PatientDTO containing updated information.</param>
        Task UpdatePatientInfoAsync(string userId, PatientDTO patientDto);

        /// <summary>
        /// Retrieves a patient along with their medication schedules by patient ID and maps the result to a PatientDTO.
        /// </summary>
        /// <param name="patientId">The patient ID whose full record, including medication schedules, is to be retrieved.</param>
        /// <returns>The patient DTO with schedules if found; otherwise, null.</returns>
        Task<PatientDTO?> GetPatientWithSchedulesAsync(string patientId);
    }
}

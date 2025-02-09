namespace MyMedCalendar.Services
{
    /// <summary>
    /// Provides a centralized access point to various domain-specific services within the application.
    /// </summary>
    public interface IApplicationService
    {
        /// <summary>
        /// Gets the service responsible for managing patient-related operations.
        /// </summary>
        IPatientService PatientService { get; }

        /// <summary>
        /// Gets the service responsible for managing drug-related operations.
        /// </summary>
        IDrugService DrugService { get; }

        /// <summary>
        /// Gets the service responsible for managing medication schedule-related operations.
        /// </summary>
        IMedicationService MedicationService { get; }
    }
}

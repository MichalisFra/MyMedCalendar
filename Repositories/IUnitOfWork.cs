using Microsoft.AspNetCore.Identity;

/// <summary>
/// Defines an interface for the Unit of Work pattern, which groups all repository operations into a single transaction,
/// ensuring consistency and atomicity of the data operations across repositories.
/// </summary>
namespace MyMedCalendar.Repositories
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Gets the repository managing patient data.
        /// </summary>
        PatientRepository PatientRepository { get; }

        /// <summary>
        /// Gets the repository managing medication schedule data.
        /// </summary>
        MedicationScheduleRepository MedicationScheduleRepository { get; }

        /// <summary>
        /// Gets the repository managing drug data.
        /// </summary>
        DrugRepository DrugRepository { get; }

        /// <summary>
        /// Saves all changes made in this unit of work to the database.
        /// </summary>
        /// <returns>True if the save operation was successful and changes were made, false otherwise.</returns>
        Task<bool> SaveAsync();
    }
}

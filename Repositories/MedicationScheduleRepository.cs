using Microsoft.EntityFrameworkCore;
using MyMedCalendar.Data;
using MyMedCalendar.Models;

/// <summary>
/// Repository for performing operations specific to <see cref="MedicationSchedule"/> entities.
/// </summary>
namespace MyMedCalendar.Repositories
{
    public class MedicationScheduleRepository : BaseRepository<MedicationSchedule>, IMedicationScheduleRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MedicationScheduleRepository"/> class.
        /// </summary>
        /// <param name="context">The database context used by the repository.</param>
        public MedicationScheduleRepository(AppDbContext context) : base(context) { }

        /// <summary>
        /// Asynchronously retrieves a collection of <see cref="MedicationSchedule"/> entities by their IDs.
        /// </summary>
        /// <param name="ids">A collection of IDs for the medication schedules to retrieve.</param>
        /// <returns>A collection of medication schedules that match the provided IDs.</returns>
        public async Task<IEnumerable<MedicationSchedule>> GetByIdsAsync(IEnumerable<int> ids)
        {
            return await _context.MedicationSchedules
                .Include(ms => ms.Drug) // Include related Drug data for comprehensive information
                .Where(ms => ids.Contains(ms.Id))
                .ToListAsync();
        }

        /// <summary>
        /// Asynchronously retrieves all <see cref="MedicationSchedule"/> entities associated with a specific patient by the patient's ID.
        /// </summary>
        /// <param name="patientId">The patient ID to match medication schedules against.</param>
        /// <returns>A collection of medication schedules associated with the specified patient.</returns>
        public async Task<IEnumerable<MedicationSchedule>> GetByPatientIdAsync(string patientId)
        {
            return await _dbSet
                .Include(ms => ms.Drug) // Ensures the Drug information is loaded along with the schedule
                .Where(ms => ms.PatientId == patientId)
                .ToListAsync();
        }
    }
}

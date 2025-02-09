using Microsoft.EntityFrameworkCore;
using MyMedCalendar.Data;
using MyMedCalendar.Models;


/// <summary>
/// Repository for performing operations specific to <see cref="Patient"/> entities.
/// </summary>
namespace MyMedCalendar.Repositories
{
    public class PatientRepository : BaseRepository<Patient>, IPatientRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PatientRepository"/> class.
        /// </summary>
        /// <param name="context">The database context used by the repository.</param>
        public PatientRepository(AppDbContext context) : base(context) { }

        /// <summary>
        /// Asynchronously retrieves a patient by their user ID, including their medication schedules and the related drug information.
        /// </summary>
        /// <param name="userId">The user ID associated with the patient.</param>
        /// <returns>The patient if found; otherwise, null.</returns>
        public async Task<Patient?> GetByUserIdAsync(string userId)
        {
            return await _context.Patients
                .Include(p => p.MedicationSchedules!)
                .ThenInclude(ms => ms.Drug)
                .FirstOrDefaultAsync(p => p.UserId == userId); // Compare as string
        }

        /// <summary>
        /// Asynchronously retrieves all patients including their medication schedules.
        /// </summary>
        /// <returns>A collection of all patients with their associated medication schedules.</returns>
        public async Task<IEnumerable<Patient>> GetPatientsWithSchedulesAsync()
        {
            return await _context.Patients
                .Include(p => p.MedicationSchedules)
                .ToListAsync();
        }

        /// <summary>
        /// Asynchronously retrieves a single patient by ID, including their medication schedules and the drugs associated with those schedules.
        /// </summary>
        /// <param name="patientId">The ID of the patient to retrieve.</param>
        /// <returns>The patient with their schedules and drugs if found; otherwise, null.</returns>
        public async Task<Patient?> GetPatientWithSchedulesAsync(string patientId) // Implementation
        {
            return await _context.Patients
                .Include(p => p.MedicationSchedules!)
                .ThenInclude(ms => ms.Drug)
                .FirstOrDefaultAsync(p => p.Id == patientId);
        }
    }
}

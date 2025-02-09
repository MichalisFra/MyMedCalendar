using Microsoft.AspNetCore.Identity;
using MyMedCalendar.Data;
using MyMedCalendar.Models;


/// <summary>
/// Encapsulates a unit of work that groups all repository operations into a single transaction,
/// ensuring data consistency across operations.
/// </summary>
namespace MyMedCalendar.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            
        }

        public PatientRepository PatientRepository => new(_context);
        public DrugRepository DrugRepository => new(_context);
        public MedicationScheduleRepository MedicationScheduleRepository => new(_context);

        /// <summary>
        /// Saves all changes made in the context of the current transaction.
        /// </summary>
        /// <returns>True if at least one change was saved successfully; otherwise, false.</returns>
        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
using Microsoft.EntityFrameworkCore;
using MyMedCalendar.Data;
using MyMedCalendar.Models;

/// <summary>
/// Repository for performing operations specific to <see cref="Drug"/> entities.
/// </summary>
namespace MyMedCalendar.Repositories
{
    public class DrugRepository : BaseRepository<Drug>, IDrugRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DrugRepository"/> class.
        /// </summary>
        /// <param name="context">The database context used by the repository.</param>
        public DrugRepository(AppDbContext context) : base(context) { }

        /// <summary>
        /// Asynchronously retrieves a drug by its name.
        /// </summary>
        /// <param name="name">The name of the drug to find.</param>
        /// <returns>The drug if found; otherwise, null.</returns>
        public async Task<Drug?> GetByNameAsync(string name)
        {
            return await _dbSet.FirstOrDefaultAsync(d => d.Name.ToLower() == name.ToLower());
        }

        /// <summary>
        /// Asynchronously retrieves all drugs made by a specified company.
        /// </summary>
        /// <param name="companyName">The name of the company to match drugs against.</param>
        /// <returns>A collection of drugs produced by the specified company.</returns>
        public async Task<IEnumerable<Drug>> GetByCompanyAsync(string companyName)
        {
            return await _dbSet.Where(d => d.Company.ToLower() == companyName.ToLower()).ToListAsync();
        }
    }
}

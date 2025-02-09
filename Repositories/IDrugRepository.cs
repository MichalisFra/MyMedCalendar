using MyMedCalendar.Models;

/// <summary>
/// Defines an interface for a repository that handles operations specific to <see cref="Drug"/> entities.
/// </summary>
namespace MyMedCalendar.Repositories
{
    public interface IDrugRepository : IBaseRepository<Drug>
    {
        /// <summary>
        /// Asynchronously retrieves a drug by its name.
        /// </summary>
        /// <param name="name">The name of the drug to find.</param>
        /// <returns>The drug if found; otherwise, null.</returns>
        Task<Drug?> GetByNameAsync(string name);

        /// <summary>
        /// Asynchronously retrieves all drugs manufactured by a specified company.
        /// </summary>
        /// <param name="companyName">The name of the company whose drugs are to be retrieved.</param>
        /// <returns>A collection of drugs manufactured by the specified company.</returns>
        Task<IEnumerable<Drug>> GetByCompanyAsync(string companyName);
    }
}

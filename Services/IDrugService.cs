using MyMedCalendar.DTOs;

namespace MyMedCalendar.Services
{
    /// <summary>
    /// Defines a contract for services that manage drug-related operations, providing methods
    /// for querying and updating drug information in the system.
    /// </summary>
    public interface IDrugService
    {
        /// <summary>
        /// Retrieves all drugs in the system.
        /// </summary>
        /// <returns>A collection of all drug DTOs.</returns>
        Task<IEnumerable<DrugDTO>> GetAllDrugsAsync();

        /// <summary>
        /// Retrieves a specific drug by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the drug.</param>
        /// <returns>A drug DTO if found; otherwise, null.</returns>
        Task<DrugDTO?> GetDrugByIdAsync(int id);

        /// <summary>
        /// Retrieves a drug by its name.
        /// </summary>
        /// <param name="name">The name of the drug.</param>
        /// <returns>A drug DTO if found; otherwise, null.</returns>
        Task<DrugDTO?> GetDrugByNameAsync(string name);

        /// <summary>
        /// Retrieves all drugs manufactured by a specific company.
        /// </summary>
        /// <param name="companyName">The name of the company.</param>
        /// <returns>A collection of drug DTOs produced by the specified company.</returns>
        Task<IEnumerable<DrugDTO>> GetDrugsByCompanyAsync(string companyName);

        /// <summary>
        /// Adds a new drug to the system.
        /// </summary>
        /// <param name="drugDto">The drug DTO containing the information needed to create a new drug.</param>
        Task AddDrugAsync(CreateDrugDTO drugDto);
    }
}

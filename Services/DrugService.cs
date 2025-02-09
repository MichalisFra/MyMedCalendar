using AutoMapper;
using MyMedCalendar.DTOs;
using MyMedCalendar.Exceptions;
using MyMedCalendar.Models;
using MyMedCalendar.Repositories;

namespace MyMedCalendar.Services
{
    /// <summary>
    /// Provides services for managing drugs, including CRUD operations and DTO mapping.
    /// </summary>
    public class DrugService : IDrugService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="DrugService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work handling data operations.</param>
        /// <param name="mapper">The AutoMapper instance used for converting between entities and DTOs.</param>
        public DrugService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all drugs from the repository and maps them to their corresponding DTOs.
        /// </summary>
        /// <returns>A collection of drug DTOs.</returns>
        public async Task<IEnumerable<DrugDTO>> GetAllDrugsAsync()
        {
            var drugs = await _unitOfWork.DrugRepository.GetAllAsync();
            if (drugs == null || !drugs.Any())
            {
                Console.WriteLine("No drugs found in the repository!");
                return new List<DrugDTO>();
            }

            Console.WriteLine($"Retrieved {drugs.Count()} drugs from the database.");
            return _mapper.Map<IEnumerable<DrugDTO>>(drugs);
        }

        /// <summary>
        /// Retrieves a drug by its ID and maps it to a DTO.
        /// </summary>
        /// <param name="id">The ID of the drug to retrieve.</param>
        /// <returns>The mapped drug DTO, or null if the drug is not found.</returns>
        public async Task<DrugDTO?> GetDrugByIdAsync(int id)
        {
            var drug = await _unitOfWork.DrugRepository.GetByIdAsync(id);
            return drug == null ? null : _mapper.Map<DrugDTO>(drug);
        }

        /// <summary>
        /// Retrieves a drug by name and maps it to a DTO.
        /// </summary>
        /// <param name="name">The name of the drug to retrieve.</param>
        /// <returns>The mapped drug DTO, or null if no drug is found.</returns>
        public async Task<DrugDTO?> GetDrugByNameAsync(string name)
        {
            var drug = await _unitOfWork.DrugRepository.GetByNameAsync(name);
            return drug == null ? null : _mapper.Map<DrugDTO>(drug);
        }

        /// <summary>
        /// Retrieves all drugs manufactured by a specific company and maps them to DTOs.
        /// </summary>
        /// <param name="companyName">The name of the company whose drugs are to be retrieved.</param>
        /// <returns>A collection of drug DTOs.</returns>
        public async Task<IEnumerable<DrugDTO>> GetDrugsByCompanyAsync(string companyName)
        {
            var drugs = await _unitOfWork.DrugRepository.GetByCompanyAsync(companyName);
            return _mapper.Map<IEnumerable<DrugDTO>>(drugs);
        }

        /// <summary>
        /// Adds a new drug to the repository.
        /// </summary>
        /// <param name="drugDto">The drug DTO to add.</param>
        public async Task AddDrugAsync(CreateDrugDTO drugDto)
        {
            if (drugDto == null) throw new InvalidArgumentException("", "Drug is empty");

            var drugEntity = _mapper.Map<Drug>(drugDto);
            await _unitOfWork.DrugRepository.AddAsync(drugEntity);
            await _unitOfWork.SaveAsync();
        }
    }
}

using AutoMapper;
using MyMedCalendar.Repositories;

namespace MyMedCalendar.Services
{
    /// <summary>
    /// Central service class that provides access to domain-specific services such as patient, drug, and medication services.
    /// </summary>
    public class ApplicationService : IApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work that manages repository transactions.</param>
        /// <param name="mapper">The AutoMapper instance used for entity-DTO mappings.</param>
        public ApplicationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the patient service instance, providing operations related to patient management.
        /// </summary>
        public IPatientService PatientService => new PatientService(_unitOfWork, _mapper);

        /// <summary>
        /// Gets the drug service instance, providing operations related to drug management.
        /// </summary>
        public IDrugService DrugService => new DrugService(_unitOfWork, _mapper);

        /// <summary>
        /// Gets the medication service instance, providing operations related to medication schedules.
        /// </summary>
        public IMedicationService MedicationService => new MedicationService(_unitOfWork, _mapper);
    }
}

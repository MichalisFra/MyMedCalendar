using AutoMapper;
using MyMedCalendar.DTOs;
using MyMedCalendar.Exceptions;
using MyMedCalendar.Repositories;

namespace MyMedCalendar.Services
{
    /// <summary>
    /// Provides services for managing patient data, including retrieval and updates of patient information.
    /// </summary>
    public class PatientService : IPatientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work handling data operations.</param>
        /// <param name="mapper">The AutoMapper instance used for converting between entities and DTOs.</param>
        public PatientService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves a patient by user ID and maps the result to a PatientDTO.
        /// </summary>
        /// <param name="userId">The user ID associated with the patient.</param>
        /// <returns>The patient DTO if found; otherwise, null.</returns>
        public async Task<PatientDTO?> GetPatientByUserIdAsync(string userId)
        {
            Console.WriteLine($"Fetching Patient for User ID: {userId}");

            var patient = await _unitOfWork.PatientRepository.FindAsync(p => p.UserId == userId);
            if (patient == null || !patient.Any())
                return null;

            return _mapper.Map<PatientDTO>(patient.First());
        }

        /// <summary>
        /// Updates patient information based on a provided PatientDTO.
        /// </summary>
        /// <param name="userId">The user ID of the patient whose information is to be updated.</param>
        /// <param name="patientDto">The PatientDTO containing updated patient information.</param>
        public async Task UpdatePatientInfoAsync(string userId, PatientDTO patientDto)
        {
            var patient = await _unitOfWork.PatientRepository.GetByUserIdAsync(userId);

            if (patient == null)
                throw new EntityNotFoundException("Patient", "This user is not a patient");

            patient.FirstName = patientDto.FirstName;
            patient.LastName = patientDto.LastName;
            patient.AMKA = patientDto.AMKA;
            patient.DateOfBirth = patientDto.DateOfBirth;

            patient.ModifiedAt = DateTime.UtcNow;

            _unitOfWork.PatientRepository.Update(patient);
            await _unitOfWork.SaveAsync();
        }

        /// <summary>
        /// Retrieves a patient along with their medication schedules by patient ID and maps the result to a PatientDTO.
        /// </summary>
        /// <param name="patientId">The patient ID whose full record is to be retrieved.</param>
        /// <returns>The patient DTO with schedules if found; otherwise, null.</returns>
        public async Task<PatientDTO?> GetPatientWithSchedulesAsync(string patientId)
        {
            var patient = await _unitOfWork.PatientRepository.GetPatientWithSchedulesAsync(patientId);
            if (patient == null)
            {
                return null;
            }

            return _mapper.Map<PatientDTO>(patient);
        }
    }
}

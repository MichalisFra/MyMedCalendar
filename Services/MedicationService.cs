using AutoMapper;
using MyMedCalendar.DTOs;
using MyMedCalendar.Exceptions;
using MyMedCalendar.Models;
using MyMedCalendar.Repositories;

namespace MyMedCalendar.Services
{
    /// <summary>
    /// Provides services for managing medication schedules, including operations to add, update, delete,
    /// and retrieve schedules, as well as generating calendar events based on those schedules.
    /// </summary>
    public class MedicationService : IMedicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MedicationService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work handling data operations.</param>
        /// <param name="mapper">The AutoMapper instance used for converting between entities and DTOs.</param>
        public MedicationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves medication schedules for a patient by their patient ID and maps them to DTOs.
        /// </summary>
        /// <param name="patientId">The patient ID whose schedules are to be retrieved.</param>
        /// <returns>A collection of medication schedule DTOs.</returns>
        public async Task<IEnumerable<MedicationScheduleDTO>> GetSchedulesByPatientIdAsync(string patientId)
        {
            var schedules = await _unitOfWork.MedicationScheduleRepository.GetByPatientIdAsync(patientId);
            return _mapper.Map<IEnumerable<MedicationScheduleDTO>>(schedules);
        }

        /// <summary>
        /// Adds a new medication schedule using provided DTO and associates it with a specified user.
        /// </summary>
        /// <param name="scheduleDto">The medication schedule DTO to add.</param>
        /// <param name="userId">The user ID to associate with the medication schedule.</param>
        public async Task AddScheduleAsync(MedicationScheduleDTO scheduleDto, string userId)
        {
            scheduleDto.PatientId = userId;
            var schedule = _mapper.Map<MedicationSchedule>(scheduleDto);

            var drug = await _unitOfWork.DrugRepository.GetByIdAsync(scheduleDto.DrugId);
            if (drug == null)
                throw new KeyNotFoundException("The specified drug does not exist.");

            schedule.Drug = drug;
            await _unitOfWork.MedicationScheduleRepository.AddAsync(schedule);
            await _unitOfWork.SaveAsync();
        }

        /// <summary>
        /// Deletes a medication schedule by its ID.
        /// </summary>
        /// <param name="scheduleId">The ID of the medication schedule to delete.</param>
        public async Task DeleteScheduleAsync(int scheduleId)
        {
            var schedule = await _unitOfWork.MedicationScheduleRepository.GetByIdAsync(scheduleId);
            if (schedule == null)
                throw new EntityNotFoundException("MedSchedule", "Medication Schedule was not deleted because it could not be found");

            _unitOfWork.MedicationScheduleRepository.Delete(schedule);
            await _unitOfWork.SaveAsync();
        }

        /// <summary>
        /// Retrieves medication schedules associated with a user by their user ID and maps them to DTOs.
        /// </summary>
        /// <param name="userId">The user ID whose medication schedules are to be retrieved.</param>
        /// <returns>A collection of medication schedule DTOs.</returns>
        public async Task<IEnumerable<MedicationScheduleDTO>> GetSchedulesByUserIdAsync(string userId)
        {
            var patient = await _unitOfWork.PatientRepository.GetByUserIdAsync(userId);
            if (patient == null)
                throw new EntityNotFoundException("Patient", "This user is not a patient");

            var schedules = await _unitOfWork.MedicationScheduleRepository.GetByPatientIdAsync(patient.Id.ToString());
            return _mapper.Map<IEnumerable<MedicationScheduleDTO>>(schedules);
        }

        /// <summary>
        /// Retrieves a single medication schedule by its ID and maps it to a DTO.
        /// </summary>
        /// <param name="scheduleId">The ID of the medication schedule to retrieve.</param>
        /// <returns>The medication schedule DTO, or null if not found.</returns>
        public async Task<MedicationScheduleDTO?> GetScheduleByIdAsync(int scheduleId)
        {
            var schedule = await _unitOfWork.MedicationScheduleRepository.GetByIdAsync(scheduleId);
            return schedule == null ? null : _mapper.Map<MedicationScheduleDTO>(schedule);
        }

        /// <summary>
        /// Updates an existing medication schedule using provided DTO.
        /// </summary>
        /// <param name="scheduleDto">The medication schedule DTO containing updated values.</param>
        public async Task UpdateScheduleAsync(MedicationScheduleDTO scheduleDto)
        {
            var schedule = await _unitOfWork.MedicationScheduleRepository.GetByIdAsync(scheduleDto.Id);
            if (schedule == null)
                throw new EntityNotFoundException("MedSchedule", $"Medication schedule with ID {scheduleDto.Id} not found.");

            schedule.Dosage = scheduleDto.Dosage;
            schedule.FrequencyPerWeek = scheduleDto.FrequencyPerWeek;
            schedule.StartDate = scheduleDto.StartDate;
            schedule.EndDate = scheduleDto.EndDate;

            _unitOfWork.MedicationScheduleRepository.Update(schedule);
            await _unitOfWork.SaveAsync();
        }

        /// <summary>
        /// Generates calendar events for a set of medication schedules identified by their IDs.
        /// </summary>
        /// <param name="scheduleIds">Array of schedule IDs for which to generate events.</param>
        /// <returns>A list of medication event DTOs.</returns>
        public async Task<List<MedicationEventDTO>> GenerateCalendarEventsAsync(int[] scheduleIds)
        {
            var schedules = await _unitOfWork.MedicationScheduleRepository.GetByIdsAsync(scheduleIds);

            return schedules.SelectMany(schedule =>
            {
                var events = new List<MedicationEventDTO>();
                var currentDate = schedule.StartDate;
                var interval = 7 / schedule.FrequencyPerWeek;

                while (currentDate <= schedule.EndDate)
                {
                    events.Add(new MedicationEventDTO
                    {
                        Date = currentDate,
                        Notes = $"Take {schedule.Dosage}mg of {schedule.Drug.Name}"
                    });
                    currentDate = currentDate.AddDays(interval);
                }

                return events;
            }).ToList();
        }

    }
}

using AutoMapper;
using MyMedCalendar.DTOs;
using MyMedCalendar.Models;

namespace MyMedCalendar.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Patient, PatientReadOnlyDTO>().ReverseMap();
            //CreateMap<Patient, PatientDTO>().ReverseMap();

            CreateMap<Patient, PatientDTO>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap();


            // Drug Mappings
            CreateMap<Drug, DrugDTO>().ReverseMap();

            // MedicationSchedule Mappings
            CreateMap<MedicationSchedule, MedicationScheduleDTO>()
                .ForMember(dest => dest.DrugName, opt => opt.MapFrom(src => src.Drug.Name))
                .ReverseMap();

            CreateMap<MedicationSchedule, MedicationScheduleReadOnlyDTO>()
                .ForMember(dest => dest.Patient, opt => opt.MapFrom(src => src.Patient))
                .ForMember(dest => dest.Drug, opt => opt.MapFrom(src => src.Drug))
                .ReverseMap();

            


            CreateMap<CreateDrugDTO, Drug>()
                .ForMember(dest => dest.InsertedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.ModifiedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ReverseMap();

            CreateMap<Drug, DrugDTO>().ReverseMap();
        }
    }
}

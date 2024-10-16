using AdmissionCommittee.Domain.Dto;
using AdmissionCommittee.Domain.Models;
using AutoMapper;

namespace AdmissionCommittee.Domain;

/// <summary>
/// Defines the mapping profiles for entities and DTOs using AutoMapper.
/// </summary>
public class MappingProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MappingProfile"/> class.
    /// This constructor sets up mappings between domain models and their corresponding DTOs.
    /// </summary>
    public MappingProfile()
    {
        CreateMap<Abiturient, AbiturientDto>().ReverseMap();
        CreateMap<Abiturient, AbiturientCreateDto>().ReverseMap();
        CreateMap<Application, ApplicationDto>().ReverseMap();
        CreateMap<Application, ApplicationCreateDto>().ReverseMap();
        CreateMap<ExamResult, ExamResultDto>().ReverseMap();
        CreateMap<ExamResult, ExamResultCreateDto>().ReverseMap();
        CreateMap<Speciality, SpecialityDto>().ReverseMap();
        CreateMap<Speciality, SpecialityCreateDto>().ReverseMap();
    }
}

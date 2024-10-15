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
        /// <summary>
        /// Maps between <see cref="Abiturient"/> and <see cref="AbiturientDto"/>.
        /// Also allows reverse mapping from <see cref="AbiturientDto"/> to <see cref="Abiturient"/>.
        /// </summary>
        CreateMap<Abiturient, AbiturientDto>().ReverseMap();
        /// <summary>
        /// Maps between <see cref="Abiturient"/> and <see cref="AbiturientCreateDto"/>.
        /// Also allows reverse mapping from <see cref="AbiturientCreateDto"/> to <see cref="Abiturient"/>.
        /// </summary>
        CreateMap<Abiturient, AbiturientCreateDto>().ReverseMap();
        /// <summary>
        /// Maps between <see cref="Application"/> and <see cref="ApplicationDto"/>.
        /// Also allows reverse mapping from <see cref="ApplicationDto"/> to <see cref="Application"/>.
        /// </summary>
        CreateMap<Application, ApplicationDto>().ReverseMap();
        /// <summary>
        /// Maps between <see cref="Application"/> and <see cref="ApplicationCreateDto"/>.
        /// Also allows reverse mapping from <see cref="ApplicationCreateDto"/> to <see cref="Application"/>.
        /// </summary>
        CreateMap<Application, ApplicationCreateDto>().ReverseMap();
        /// <summary>
        /// Maps between <see cref="ExamResult"/> and <see cref="ExamResultDto"/>.
        /// Also allows reverse mapping from <see cref="ExamResultDto"/> to <see cref="ExamResult"/>.
        /// </summary>
        CreateMap<ExamResult, ExamResultDto>().ReverseMap();
        /// <summary>
        /// Maps between <see cref="ExamResult"/> and <see cref="ExamResultCreateDto"/>.
        /// Also allows reverse mapping from <see cref="ExamResultCreateDto"/> to <see cref="ExamResult"/>.
        /// </summary>
        CreateMap<ExamResult, ExamResultCreateDto>().ReverseMap();
        /// <summary>
        /// Maps between <see cref="Speciality"/> and <see cref="SpecialityDto"/>.
        /// Also allows reverse mapping from <see cref="SpecialityDto"/> to <see cref="Speciality"/>.
        /// </summary>
        CreateMap<Speciality, SpecialityDto>().ReverseMap();
        /// <summary>
        /// Maps between <see cref="Speciality"/> and <see cref="SpecialityCreateDto"/>.
        /// Also allows reverse mapping from <see cref="SpecialityCreateDto"/> to <see cref="Speciality"/>.
        /// </summary>
        CreateMap<Speciality, SpecialityCreateDto>().ReverseMap();
    }
}

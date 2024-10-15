using AdmissionCommittee.Domain.Dto;
using AdmissionCommittee.Domain.Models;

namespace AdmissionCommittee.Domain.Services;

/// <summary>
/// An interface for speciality service
/// </summary>
public interface ISpecialityService : IBaseService<Speciality, SpecialityDto, SpecialityCreateDto>
{
}
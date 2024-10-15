using AdmissionCommittee.Domain.Dto;
using AdmissionCommittee.Domain.Models;
using AdmissionCommittee.Domain.Repositories;

namespace AdmissionCommittee.Domain.Services;

/// <summary>
/// Service for managing specialities offered by the university.
/// This service provides business logic for managing the lifecycle of specialities,
/// such as adding, updating, retrieving, and deleting specialities.
/// </summary>
public class SpecialityService(
    ISpecialityRepository specialityRepository) : ISpecialityService
{
    private readonly ISpecialityRepository _specialityRepository = specialityRepository;
    /// <inheritdoc />
    public IEnumerable<Speciality> GetAll()
    {
        return _specialityRepository.GetAll();
    }
    /// <inheritdoc />
    public Speciality GetById(int id)
    {
        var speciality = _specialityRepository.GetById(id);
        if (speciality == null)
            throw new KeyNotFoundException("Speciality result not found");
        return speciality;
    }
    /// <inheritdoc />
    public int Add(SpecialityCreateDto specialityDto)
    {
        if (_specialityRepository.GetAll().Any(s => s.Number == specialityDto.Number))
        {
            throw new InvalidOperationException("Speciality with this number already exists");
        }
        Speciality speciality = new()
        {
            Name = specialityDto.Name,
            Number = specialityDto.Number,
            Facility = specialityDto.Facility
        };
        return _specialityRepository.Add(speciality);
    }
    /// <inheritdoc />
    public void Update(int id, SpecialityCreateDto specialityDto)
    {
        var speciality = GetById(id);
        speciality.Name = specialityDto.Name;
        speciality.Number = specialityDto.Number;
        speciality.Facility = specialityDto.Facility;
        
        _specialityRepository.Update(id, speciality);
    }
    /// <inheritdoc />
    public void Delete(int id)
    {
        if (_specialityRepository.GetById(id) == null)
        {
            throw new KeyNotFoundException("Cannot delete a non-existing speciality");
        }
        _specialityRepository.Delete(id);
    }
}

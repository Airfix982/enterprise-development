using AdmissionCommittee.Domain.Dto;
using AdmissionCommittee.Domain.Models;
using AdmissionCommittee.Domain.Repositories;
using AutoMapper;

namespace AdmissionCommittee.Domain.Services;

/// <summary>
/// Service for managing specialities offered by the university.
/// This service provides business logic for managing the lifecycle of specialities,
/// such as adding, updating, retrieving, and deleting specialities.
/// </summary>
public class SpecialityService(
    ISpecialityRepository specialityRepository,
    IMapper mapper) : ISpecialityService
{
    private readonly ISpecialityRepository _specialityRepository = specialityRepository;
    private readonly IMapper _mapper = mapper;

    /// <inheritdoc />
    public IEnumerable<SpecialityDto> GetAll()
    {
        var specialities = _specialityRepository.GetAll();
        return _mapper.Map<IEnumerable<SpecialityDto>>(specialities);
    }

    /// <inheritdoc />
    public SpecialityDto GetById(int id)
    {
        var speciality = _specialityRepository.GetById(id);
        if (speciality == null)
            throw new KeyNotFoundException("Speciality not found");
        return _mapper.Map<SpecialityDto>(speciality);
    }

    /// <inheritdoc />
    public int Add(SpecialityCreateDto specialityDto)
    {
        if (_specialityRepository.GetAll().Any(s => s.Number == specialityDto.Number))
        {
            throw new InvalidOperationException("Speciality with this number already exists");
        }

        var speciality = _mapper.Map<Speciality>(specialityDto);
        return _specialityRepository.Add(speciality);
    }

    /// <inheritdoc />
    public void Update(int id, SpecialityCreateDto specialityDto)
    {
        var speciality = GetById(id);
        _mapper.Map(specialityDto, speciality);

        _specialityRepository.Update(_mapper.Map<Speciality>(speciality));
    }

    /// <inheritdoc />
    public void Delete(int id)
    {
        var speciality = _specialityRepository.GetById(id);
        if (speciality == null)
        {
            throw new KeyNotFoundException("Cannot delete a non-existing speciality");
        }
        _specialityRepository.Delete(id);
    }
}

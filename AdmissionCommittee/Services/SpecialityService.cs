﻿using AdmissionCommittee.Domain.Dto;
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
    public async Task<IEnumerable<SpecialityDto>> GetAllAsync()
    {
        var specialities = await _specialityRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<SpecialityDto>>(specialities);
    }

    /// <inheritdoc />
    public async Task<SpecialityDto> GetByIdAsync(int id)
    {
        var speciality = await _specialityRepository.GetByIdAsync(id);
        if (speciality == null)
            throw new KeyNotFoundException("Speciality not found");
        return _mapper.Map<SpecialityDto>(speciality);
    }

    /// <inheritdoc />
    public async Task<int> AddAsync(SpecialityCreateDto specialityDto)
    {
        if ((await _specialityRepository.GetAllAsync()).Any(s => s.Number == specialityDto.Number))
        {
            throw new InvalidOperationException("Speciality with this number already exists");
        }

        var speciality = _mapper.Map<Speciality>(specialityDto);
        return await _specialityRepository.AddAsync(speciality);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(int id, SpecialityCreateDto specialityDto)
    {
        var speciality = await GetByIdAsync(id);
        _mapper.Map(specialityDto, speciality);

        await _specialityRepository.UpdateAsync(_mapper.Map<Speciality>(speciality));
    }

    /// <inheritdoc />
    public async Task DeleteAsync(int id)
    {
        var speciality = await _specialityRepository.GetByIdAsync(id);
        if (speciality == null)
        {
            throw new KeyNotFoundException("Cannot delete a non-existing speciality");
        }
        await _specialityRepository.DeleteAsync(id);
    }
}

using AdmissionCommittee.Domain.Dto;
using AdmissionCommittee.Domain.Models;

namespace AdmissionCommittee.Domain.Services;

/// <summary>
/// An interface for abiturient service
/// </summary>
public interface IAbiturientService : IBaseService<Abiturient, AbiturientDto, AbiturientCreateDto>
{
    /// <summary>
    /// Get all abiturients from the city
    /// </summary>
    /// <param name="city">City where abiturients are from</param>
    /// <returns>A collection of abiturients</returns>
    public IEnumerable<AbiturientDto> GetAbiturientsByCity(string city);
    /// <summary>
    /// Get all abiturients older than selected age
    /// </summary>
    /// <param name="age">Age over wich the abiturient is</param>
    /// <returns>A collection of abiturients</returns>
    public IEnumerable<AbiturientDto> GetAbiturientsOlderThan(int age);
    /// <summary>
    /// Get all abiturients who selected the speciality, ordered by their exam rates
    /// </summary>
    /// <param name="specialityId">Id of an speciality of an abiturient</param>
    /// <returns>A collection of abiturients</returns>
    public IEnumerable<AbiturientDto> GetAbiturientBySpecialityOrderedByRates(int specialityId);
    /// <summary>
    /// Get abiturients count by specialities which are the favorite ones
    /// </summary>
    /// <returns>A collection of SpecialitiesCountAsFavoriteDto</returns>
    public IEnumerable<SpecialitiesCountAsFavoriteDto> GetAbiturientsCountByFirstPrioritySpecialities();
    /// <summary>
    /// Get maxCount top-rated abiturients
    /// </summary>
    /// <param name="maxCount">Max count to get of top rated abituriens</param>
    /// <returns>A collection of abiturients</returns>
    public IEnumerable<AbiturientWithExamScoresDto> GetTopRatedAbiturients(int maxCount);
    /// <summary>
    /// Get abiturients and their favorite specialities who got max rate by each exam
    /// </summary>
    /// <returns>A collection of abiturients</returns>
    public IEnumerable<AbiturientMaxRateDto> GetMaxRatedAbiturienstWithFavoriteSpeciality();
}
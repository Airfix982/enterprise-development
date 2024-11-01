using AdmissionCommittee.Tests.Fixtures;

namespace AdmissionCommittee.Tests.Tests;

/// <summary>  
/// This class contains unit tests for testing various operations related to abiturients, applications, and specialities.
/// </summary>
/// <param name="fixture">Fixture to provide data for testing.</param>
public class TestRequests(AdmissionComitteeFixture fixture) : IClassFixture<AdmissionComitteeFixture>
{
    private AdmissionComitteeFixture _fixture = fixture;

    /// <summary>
    /// Tests the selection of abiturients by city.
    /// </summary>
    [Fact]
    public async Task TestSelectAbiturientsByCity()
    {
        var query = (await _fixture.AbiturientService.GetAbiturientsByCityAsync("Samara")).Select(a => a.Id).ToList();

        Assert.Equivalent(2, query.Count);
        Assert.Equal([2, 5], query);
    }

    /// <summary>
    /// Tests the selection of abiturients who are older than 20 years.
    /// </summary>
    [Fact]
    public async Task TestSelectOlderAbiturients()
    {
        var query = (await _fixture.AbiturientService.GetAbiturientsOlderThanAsync(20)).Select(a => a.Id).ToList();

        Assert.Equivalent(4, query.Count);
        Assert.Equal([2, 4, 6, 9], query);
    }

    /// <summary>
    /// Tests the selection of abiturients by a specific speciality.
    /// </summary>
    [Fact]
    public async Task TestSelectBySpeciality()
    {
        var query = (await _fixture.AbiturientService.GetAbiturientBySpecialityOrderedByRatesAsync(1))
            .Select(a => a.Id).ToList();

        Assert.Equivalent(1, query.Count);
        Assert.Equal([5], query);
    }

    /// <summary>
    /// Tests the number of abiturients who applied to specific specialities with first priority.
    /// </summary>
    [Fact]
    public async Task TestFirstPrioritySpecialitiesByAbiturientsAmount()
    {
        var query = (await _fixture.AbiturientService.GetAbiturientsCountByFirstPrioritySpecialitiesAsync()).ToList();
        
        Assert.Collection(query,
            item =>
            {
                Assert.Equal(1, item.SpecialityId);
                Assert.Equal(1, item.AbiturientsCount);
            },
            item =>
            {
                Assert.Equal(2, item.SpecialityId);
                Assert.Equal(1, item.AbiturientsCount);
            },
            item =>
            {
                Assert.Equal(3, item.SpecialityId);
                Assert.Equal(1, item.AbiturientsCount);
            },
            item =>
            {
                Assert.Equal(4, item.SpecialityId);
                Assert.Equal(1, item.AbiturientsCount);
            },
            item =>
            {
                Assert.Equal(5, item.SpecialityId);
                Assert.Equal(1, item.AbiturientsCount);
            },
            item =>
            {
                Assert.Equal(6, item.SpecialityId);
                Assert.Equal(0, item.AbiturientsCount);
            },
            item =>
            {
                Assert.Equal(7, item.SpecialityId);
                Assert.Equal(1, item.AbiturientsCount);
            },
            item =>
            {
                Assert.Equal(8, item.SpecialityId);
                Assert.Equal(1, item.AbiturientsCount);
            },
            item =>
            {
                Assert.Equal(9, item.SpecialityId);
                Assert.Equal(1, item.AbiturientsCount);
            },
            item =>
            {
                Assert.Equal(10, item.SpecialityId);
                Assert.Equal(0, item.AbiturientsCount);
            }
            );
    }

    /// <summary>
    /// Tests the selection of top 5 abiturients based on their exam scores.
    /// </summary>
    [Fact]
    public async Task TestTopRatedAbiturients()
    {
        var query = (await _fixture.AbiturientService.GetTopRatedAbiturientsAsync(5)).Select(a => a.Abiturient.Id).ToList();

        Assert.Equivalent(5, query.Count);
        Assert.Equal([3, 9, 7, 5, 1], query);
    }

    /// <summary>
    /// Tests the favourite specialities chosen by the top-rated abiturients based on maximum exam scores.
    /// </summary>
    [Fact]
    public async Task TestFavoriteSpecialitiesByTopRatedAbiturients()
    {
        var query = (await _fixture.AbiturientService.GetMaxRatedAbiturientsWithFavoriteSpecialityAsync()).ToList();

        Assert.Equivalent(5, query.Count);
        Assert.Equal([3, 4, 5, 7, 9],
                     query.Select(q => q.Abiturient.Id).ToList());

        Assert.Equal([0, 8, 1, 4, 9], query
            .Select(q => q.FavoriteSpecialityId).ToList());
    }
}

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
    public void TestSelectAbiturientsByCity()
    {
        var query = _fixture.AbiturientService.GetAbiturientsByCity("Samara").Select(a => a.Id).ToList();

        Assert.Equivalent(2, query.Count);
        Assert.Equal([1, 4], query);
    }

    /// <summary>
    /// Tests the selection of abiturients who are older than 20 years.
    /// </summary>
    [Fact]
    public void TestSelectOlderAbiturients()
    {
        var query = _fixture.AbiturientService.GetAbiturientsOlderThan(20).Select(a => a.Id).ToList();

        Assert.Equivalent(4, query.Count);
        Assert.Equal([1, 3, 5, 8], query);
    }

    /// <summary>
    /// Tests the selection of abiturients by a specific speciality.
    /// </summary>
    [Fact]
    public void TestSelectBySpeciality()
    {
        var query = _fixture.AbiturientService.GetAbiturientBySpecialityOrderedByRates(1)
            .Select(a => a.Id).ToList();

        Assert.Equivalent(2, query.Count);
        Assert.Equal([5, 0], query);
    }

    /// <summary>
    /// Tests the number of abiturients who applied to specific specialities with first priority.
    /// </summary>
    [Fact]
    public void TestFirstPrioritySpecialitiesByAbiturientsAmount()
    {
        var query = _fixture.AbiturientService.GetAbiturientsCountByFirstPrioritySpecialities().ToList();

        Assert.Collection(query,
            item =>
            {
                Assert.Equal(0, item.SpecialityId);
                Assert.Equal(2, item.AbiturientsCount);
            },
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
            }
            );
    }

    /// <summary>
    /// Tests the selection of top 5 abiturients based on their exam scores.
    /// </summary>
    [Fact]
    public void TestTopRatedAbiturients()
    {
        var query = _fixture.AbiturientService.GetTopRatedAbiturients(5).Select(a => a.Abiturient.Id).ToList();

        Assert.Equivalent(5, query.Count);
        Assert.Equal([3, 9, 7, 5, 1], query);
    }

    /// <summary>
    /// Tests the favourite specialities chosen by the top-rated abiturients based on maximum exam scores.
    /// </summary>
    [Fact]
    public void TestFavoriteSpecialitiesByTopRatedAbiturients()
    {
        var query = _fixture.AbiturientService.GetMaxRatedAbiturientsWithFavoriteSpeciality().ToList();

        Assert.Equivalent(5, query.Count);
        Assert.Equal([3, 4, 5, 7, 9],
                     query.Select(q => q.Abiturient.Id).ToList());

        Assert.Equal([0, 8, 1, 4, 9], query
            .Select(q => q.FavoriteSpecialityId).ToList());
    }
}

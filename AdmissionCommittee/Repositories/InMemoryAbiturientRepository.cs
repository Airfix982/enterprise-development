﻿using AdmissionCommittee.Domain.Models;

namespace AdmissionCommittee.Domain.Repositories;

/// <summary>
/// In-memory repository for managing abiturients.
/// </summary>
public class InMemoryAbiturientRepository : RepositoryInMemory<Abiturient>, IAbiturientRepository
{
    /// <summary>
    /// Default constructor that initializes an empty repository.
    /// </summary>
    public InMemoryAbiturientRepository() : base() { }

    /// <summary>
    /// Constructor that initializes the repository with a predefined list of abiturients.
    /// </summary>
    /// <param name="initData">Initial list of abiturients.</param>

    public InMemoryAbiturientRepository(List<Abiturient> initData) : base(initData) { }

    /// <summary>
    /// Updates an existing abiturient's details in the repository.
    /// </summary>
    /// <param name="abiturient">Abiturient object with updated information.</param>
    public override Task UpdateAsync(Abiturient abiturient)
    {
        var existingAbiturient = GetByIdAsync(abiturient.Id).Result;
        if (existingAbiturient != null)
        {
            existingAbiturient.Name = abiturient.Name;
            existingAbiturient.LastName = abiturient.LastName;
            existingAbiturient.BirthdayDate = abiturient.BirthdayDate;
            existingAbiturient.Country = abiturient.Country;
            existingAbiturient.City = abiturient.City;
        }
        return Task.CompletedTask;
    }
}

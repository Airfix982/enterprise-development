using AdmissionCommittee.Domain.Models;

namespace AdmissionCommittee.Domain.Repositories;

/// <summary>
/// Base repository interface
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRepository<T> where T : class, IEntity
{
    /// <summary>
    /// Retrieves an entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity.</param>
    /// <returns>The entity with the specified identifier, or null if not found.</returns>
    public T? GetById(int id);
    /// <summary>
    /// Get all objects
    /// </summary>
    /// <returns>IEnumerable<typeparamref name="T"/></returns>
    public IEnumerable<T> GetAll();
    /// <summary>
    /// Updates an existing entity in the in-memory context.
    /// </summary>
    /// <param name="entity">The entity with updated values.</param>
    /// <param name="id">The entity's id'.</param>
    public void Update(int id, T entity);
    /// <summary>
    /// Adds a new entity to the in-memory context.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <returns>The added entity's id.</returns>
    public int Add(T entity);
    /// <summary>
    /// Deletes an entity from the in-memory context by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to delete.</param>
    public void Delete(int id);
}

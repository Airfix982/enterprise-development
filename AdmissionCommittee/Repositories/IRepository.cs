using AdmissionCommittee.Domain.Models;
using System.Threading.Tasks;

namespace AdmissionCommittee.Domain.Repositories;

/// <summary>
/// Base repository interface
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRepository<T> where T : class, IEntity
{
    /// <summary>
    /// Asynchronously retrieves an entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the entity with the specified identifier, or null if not found.</returns>
    public Task<T?> GetByIdAsync(int id);

    /// <summary>
    /// Asynchronously gets all objects.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains an IEnumerable of <typeparamref name="T"/>.</returns>
    public Task<IEnumerable<T>> GetAllAsync();

    /// <summary>
    /// Asynchronously updates an existing entity in the context.
    /// </summary>
    /// <param name="entity">The entity with updated values.</param>
    public Task UpdateAsync(T entity);

    /// <summary>
    /// Asynchronously adds a new entity to the context.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the added entity's id.</returns>
    public Task<int> AddAsync(T entity);

    /// <summary>
    /// Asynchronously deletes an entity from the context by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to delete.</param>
    public Task DeleteAsync(int id);
}

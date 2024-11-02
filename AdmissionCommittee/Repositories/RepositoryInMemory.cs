using AdmissionCommittee.Domain.Models;

namespace AdmissionCommittee.Domain.Repositories;

/// <summary>
/// Generic repository implementation for managing in-memory collections of entities.
/// </summary>
/// <typeparam name="T">The type of the entity.</typeparam>
public class RepositoryInMemory<T> : IRepository<T> where T : class, IEntity
{
    private int _currentId;
    /// <summary>
    /// The in-memory context for storing entities of type T.
    /// </summary>
    protected readonly List<T> _context;
    /// <summary>
    /// Initializes a new instance of the <see cref="RepositoryInMemory{T}"/> class.
    /// </summary>
    public RepositoryInMemory()
    {
        _context = new List<T>();
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="RepositoryInMemory{T}"/> class.
    /// </summary>
    public RepositoryInMemory(List<T> initData)
    {
        _context = initData;
    }
    /// <inheritdoc />
    public Task<IEnumerable<T>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<T>>(_context);
    }
    /// <inheritdoc />
    public Task<T?> GetByIdAsync(int id)
    {
        return Task.FromResult(_context.FirstOrDefault(entity => entity.Id == id));
    }
    /// <inheritdoc />
    public Task<int> AddAsync(T entity)
    {
        entity.Id = _currentId++;
        _context.Add(entity);
        return Task.FromResult(entity.Id);
    }
    /// <inheritdoc />
    public virtual Task UpdateAsync(T entity)
    {
        return Task.CompletedTask;
    }
    /// <inheritdoc />
    public Task DeleteAsync(int id)
    {
        var entity = GetByIdAsync(id).Result;
        if (entity != null)
            _context.Remove(entity);
        return Task.CompletedTask;
    }
}

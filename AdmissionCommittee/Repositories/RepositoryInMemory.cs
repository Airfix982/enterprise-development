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
    public IEnumerable<T> GetAll() => _context;
    /// <inheritdoc />
    public T? GetById(int id)
    {
        return _context.FirstOrDefault(entity => entity.Id == id);
    }
    /// <inheritdoc />
    public int Add(T entity)
    {
        entity.Id = _currentId++;
        _context.Add(entity);
        return entity.Id;
    }
    /// <inheritdoc />
    public virtual void Update(int id, T entity)
    {
    }
    /// <inheritdoc />
    public void Delete(int id)
    {
        var entity = GetById(id);
        if (entity != null)
            _context.Remove(entity);
    }
}

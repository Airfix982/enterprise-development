using AdmissionCommittee.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdmissionCommittee.Domain.Repositories
{
    /// <summary>
    /// Generic repository implementation for managing in-memory collections of entities.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    public class RepositoryInMemory<T> : IRepositoryInMemory<T> where T : class, IEntity
    {
        /// <summary>
        /// The in-memory context for storing entities of type T.
        /// </summary>
        protected readonly List<T> _context;
        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T}"/> class.
        /// </summary>
        public RepositoryInMemory()
        {
            _context = new();
        }
        /// <inheritdoc />
        public IEnumerable<T> GetAll() => _context;
        /// <inheritdoc />
        public T? GetById(int id)
        {
            return _context.FirstOrDefault(entity => entity.Id == id);
        }
        /// <inheritdoc />
        public void Add(T entity)
        {
            _context.Add(entity);
        }
        /// <inheritdoc />
        public void Update(T entity)
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
}

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
        /// <summary>
        /// Retrieves all entities from the in-memory context.
        /// </summary>
        /// <returns>An IEnumerable collection of all entities.</returns>
        public IEnumerable<T> GetAll() => _context;
        /// <summary>
        /// Retrieves an entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <returns>The entity with the specified identifier, or null if not found.</returns>
        public T? GetById(int id)
        {
            return _context.FirstOrDefault(entity => entity.Id == id);
        }
        /// <summary>
        /// Adds a new entity to the in-memory context.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        public void Add(T entity)
        {
            _context.Add(entity);
        }
        /// <summary>
        /// Updates an existing entity in the in-memory context.
        /// </summary>
        /// <param name="entity">The entity with updated values.</param>
        public void Update(T entity)
        {
        }
        /// <summary>
        /// Deletes an entity from the in-memory context by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to delete.</param>
        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity != null)
                _context.Remove(entity);
        }
    }
}

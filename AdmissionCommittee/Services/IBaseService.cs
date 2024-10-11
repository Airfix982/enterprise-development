using AdmissionCommittee.Domain.Dto;
using AdmissionCommittee.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionCommittee.Domain.Services
{
    /// <summary>
    /// Represents a generic base service interface for CRUD operations.
    /// </summary>
    /// <typeparam name="T">The domain entity type that implements <see cref="IEntity"/>.</typeparam>
    /// <typeparam name="TDto">The data transfer object (DTO) type that implements <see cref="IDto"/>.</typeparam>
    public interface IBaseService<T, TDto>
        where T : class, IEntity
        where TDto : class, IDto    
    {
        /// <summary>
        /// Retrieves all entities of type <typeparamref name="T"/>.
        /// </summary>
        /// <returns>An enumerable collection of all entities.</returns>
        IEnumerable<T> GetAll();
        /// <summary>
        /// Retrieves a specific entity of type <typeparamref name="T"/> by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <returns>The entity if found; otherwise, null.</returns>
        T? GetById(int id);
        /// <summary>
        /// Adds a new entity based on the provided data transfer object (DTO) of type <typeparamref name="TDto"/>.
        /// </summary>
        /// <param name="dto">The data transfer object (DTO) containing the data for the new entity.</param>
        void Add(TDto dto);
        /// <summary>
        /// Updates an existing entity using the provided data transfer object (DTO) of type <typeparamref name="TDto"/>.
        /// </summary>
        /// <param name="dto">The data transfer object (DTO) containing updated data for the entity.</param>
        void Update(TDto dto);
        /// <summary>
        /// Deletes an entity of type <typeparamref name="T"/> by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to be deleted.</param>
        void Delete(int id);
    }
}

using AdmissionCommittee.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionCommittee.Domain.Repositories
{
    /// <summary>
    /// Base repository interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepositoryInMemory<T> where T : class, IEntity
    {
        /// <summary>
        /// Get object by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns><typeparamref name="T"/></returns>
        public T? GetById(int id);
        /// <summary>
        /// Get all objects
        /// </summary>
        /// <returns>IEnumerable<typeparamref name="T"/></returns>
        public IEnumerable<T> GetAll();
        /// <summary>
        /// Update object
        /// </summary>
        /// <param name="entity"></param>
        public void Update(T entity);
        /// <summary>
        /// Add an object
        /// </summary>
        /// <param name="entity"></param>
        public void Add(T entity);
        /// <summary>
        /// Delete an object
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(int id);
    }
}

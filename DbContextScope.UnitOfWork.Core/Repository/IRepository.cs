using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DbContextScope.UnitOfWork.Core.Repository
{
    /// <summary>
    /// Defines a generic repository that can be implemented
    /// using any data store or library.
    /// </summary>
    /// <typeparam name="TEntity">Type of the entities that the repository will manage.</typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Adds the given entity to the Unit of Work such that it will be inserted
        /// into the data store when Save is called on the Unit of Work.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        void Add(TEntity entity);

        /// <summary>
        /// Returns the <see cref="IQueryable"/> representation of the data store.
        /// </summary>
        /// <returns>An <see cref="IQueryable"/> that represents the data store.</returns>
        IQueryable<TEntity> AsQueryable();

        /// <summary>
        /// Attaches the given entity to the Unit of Work. That is, the entity is placed into
        /// the Unit of Work in the Unchanged state, just as if it had been read from the data store.
        /// </summary>
        /// <param name="entity">The entity to attach.</param>
        void Attach(TEntity entity);

        /// <summary>
        /// Marks the given entity as Deleted such that it will be deleted from the data store when
        /// Save is called. Note that the entity must exist in the Unit of Work in some other state
        /// before this method is called.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Marks the given entity as Modified such that its changes will be saved when Save is called.
        /// </summary>
        /// <param name="entity">The entity to mark as Modified.</param>
        void Edit(TEntity entity);

        /// <summary>
        /// Returns the first entity in the data store that satisfies a specified condition. If no
        /// condition was specified, returns the first entity in the data store.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>
        /// The first entity in the data store that passes the test in the specified predicate
        /// function. If no condition was specified, the first entity in the data store.
        /// </returns>
        TEntity First(Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// Returns the first entity in the data store that satisfies a specified condition. If no
        /// condition was specified, returns the first entity in the data store. If no such entity
        /// is found, retuns a default value.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>
        /// The first entity in the data store that passes the test in the specified predicate
        /// function. If no condition was specified, the first entity in the data store. If
        /// no entity passes the test in the predicate, a default value.
        /// </returns>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// Asynchronously returns the first entity in the data store that satisfies a specified
        /// condition. If no condition was specified, returns the first entity in the data store.
        /// If no such entity is found, retuns a default value.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>
        /// A task that represents the first entity in the data store that passes the test in the
        /// specified predicate function. If no condition was specified, a task that represents the
        /// first entity in the data store. If no entity passes the test in the predicate, task that
        /// represents a default value.
        /// </returns>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null);

        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includeProperties);

        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includeProperties);

        TEntity LastOrDefault(Expression<Func<TEntity, bool>> predicate = null);

        TEntity Single(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate);

        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        void Update(TEntity entityToUpdate);
    }
}

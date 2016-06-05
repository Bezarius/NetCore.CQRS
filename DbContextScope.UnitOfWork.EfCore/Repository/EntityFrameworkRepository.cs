using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DbContextScope.EfCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DbContextScope.UnitOfWork.EfCore.Repository
{
    /// <summary>
    /// Implements the IEntityFrameworkRepository<TEntity> interface to represent an Entity Framework repository.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TContext"></typeparam>
    public class EntityFrameworkRepository<TEntity, TContext> : IEntityFrameworkRepository<TEntity, TContext>
        where TEntity : class
        where TContext : DbContext
    {
        #region Attributes

        readonly IAmbientDbContextLocator dbContextLocator;

        #endregion

        #region IEntityFrameworkRepository<TEntity, TContext> Implementation

        /// <summary>
        /// Constructor that receives a DbContextLocator instance.
        /// </summary>
        /// <param name="dbContextLocator">DbContextLocator instance.</param>
        public EntityFrameworkRepository(IAmbientDbContextLocator dbContextLocator)
        {
            if (dbContextLocator == null) throw new ArgumentNullException(nameof(dbContextLocator));

            this.dbContextLocator = dbContextLocator;
        }

        public virtual void Add(TEntity entity)
        {
            GetDbSet().Add(entity);
        }

        public IQueryable<TEntity> AsQueryable()
        {
            return GetDbSet().AsQueryable();
        }

        public virtual void Attach(TEntity entity)
        {
            GetDbSet().Attach(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            if (GetDbContext().Entry(entity).State == EntityState.Detached)
            {
                GetDbSet().Attach(entity);
            }
            GetDbSet().Remove(entity);
        }

        public virtual void Edit(TEntity entity)
        {
            GetDbContext().Entry(entity).State = EntityState.Modified;
        }

        public virtual TEntity First(Expression<Func<TEntity, bool>> predicate = null)
        {
            return GetDbSet().First(predicate);
        }

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate = null)
        {
            return GetDbSet().FirstOrDefault(predicate);
        }

        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return await GetDbSet().FirstOrDefaultAsync(predicate);
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return PrepareGetQuery(predicate, orderBy, includeProperties).ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return await PrepareGetQuery(predicate, orderBy, includeProperties).ToListAsync();
        }

        public virtual TEntity LastOrDefault(Expression<Func<TEntity, bool>> predicate = null)
        {
            return GetDbSet().LastOrDefault(predicate);
        }

        public virtual TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return GetDbSet().Single(predicate);
        }

        public virtual async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetDbSet().SingleAsync(predicate);
        }

        public virtual TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return GetDbSet().SingleOrDefault(predicate);
        }

        public virtual async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetDbSet().SingleOrDefaultAsync(predicate);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            GetDbSet().Attach(entityToUpdate);
            GetDbContext().Entry(entityToUpdate).State = EntityState.Modified;
        }

        #endregion

        #region Private Methods

        DbContext GetDbContext()
        {
            return dbContextLocator.Get<TContext>();
        }

        DbSet<TEntity> GetDbSet()
        {
            return dbContextLocator.Get<TContext>().Set<TEntity>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        IQueryable<TEntity> PrepareGetQuery(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }

            return query;
        }

        #endregion
    }
}

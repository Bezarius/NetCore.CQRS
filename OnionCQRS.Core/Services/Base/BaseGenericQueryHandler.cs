using System;
using System.Collections.Generic;
using System.Linq;
using DbContextScope.EfCore.Interfaces;
using MediatR;
using ODataWithOnionCQRS.Core.Query;
using OnionCQRS.Core.Data;
using OnionCQRS.Core.Extensions;

namespace OnionCQRS.Core.Services.Base
{
    public abstract class BaseGenericQueryHandler<TEntity, TDbContext> : IRequestHandler<GenericQuery<TEntity>, IEnumerable<TEntity>>
        where TEntity : class
        where TDbContext : class, IDbContext
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;

        public BaseGenericQueryHandler(IDbContextScopeFactory dbContextScopeFactory)
        {
            if (dbContextScopeFactory == null)
                throw new ArgumentNullException("dbContextScopeFactory");

            _dbContextScopeFactory = dbContextScopeFactory;
        }

        public IEnumerable<TEntity> Handle(GenericQuery<TEntity> args)
        {
            using (var dbContextScope = _dbContextScopeFactory.CreateReadOnly())
            {
                IDbContext dbCtx = dbContextScope.DbContexts.GetByInterface<TDbContext>();

                // todo: Figure out the equivalent measurement to suppress proxy generation in EF7.
                //((DbContext)dbCtx).Configuration.ProxyCreationEnabled = false;

                IQueryable<TEntity> entities = dbCtx.Set<TEntity>();

                entities = entities.Include(args);
                entities = entities.Where(args);
                entities = entities.OrderBy(args);

                // Depending on your needs, you may not want to have .Take be mandatory
                return entities.Take(args.PageSize).ToList();
            }
        }
    }
}

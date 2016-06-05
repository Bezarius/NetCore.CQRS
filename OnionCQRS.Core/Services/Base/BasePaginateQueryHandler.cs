using System;
using System.Linq;
using DbContextScope.EfCore.Interfaces;
using MediatR;
using OnionCQRS.Core.Data;
using OnionCQRS.Core.Extensions;
using OnionCQRS.Core.Query;

namespace OnionCQRS.Core.Services.Base
{
    public abstract class BasePaginateQueryHandler<TEntity, TDbContext> : IRequestHandler<PaginateQuery<TEntity>, PaginatedList<TEntity>>
        where TEntity : class
        where TDbContext : class, IDbContext
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;

        public BasePaginateQueryHandler(IDbContextScopeFactory dbContextScopeFactory)
        {
            if (dbContextScopeFactory == null)
                throw new ArgumentNullException(nameof(dbContextScopeFactory));

            _dbContextScopeFactory = dbContextScopeFactory;
        }

        public PaginatedList<TEntity> Handle(PaginateQuery<TEntity> args)
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
                var total = entities.Count();
                entities = entities.Paginate(args);

                return new PaginatedList<TEntity>(entities.ToList(), args.PageIndex, args.PageSize, total);
            }
        }
    }
}

using DbContextScope.EfCore.Interfaces;
using OnionCQRS.Core.Data;
using OnionCQRS.Core.Services.Base;

namespace OnionCQRS.Services.Query
{
    public class PaginateQueryHandler<TEntity> : BasePaginateQueryHandler<TEntity, ICompanyDbContext>
        where TEntity : class
    {
        public PaginateQueryHandler(IDbContextScopeFactory dbContextScopeFactory)
            : base(dbContextScopeFactory) { }
    }
}

using DbContextScope.EfCore.Interfaces;
using OnionCQRS.Core.Data;
using OnionCQRS.Core.Services.Base;

namespace OnionCQRS.Services.Query
{
    public class GenericQueryHandler<TEntity> : BaseGenericQueryHandler<TEntity, ICompanyDbContext>
        where TEntity : class
    {
        public GenericQueryHandler(IDbContextScopeFactory dbContextScopeFactory)
            : base(dbContextScopeFactory) { }
    }
}

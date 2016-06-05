using System;
using System.Linq.Expressions;

namespace OnionCQRS.Core.Services
{
    public interface IFilterQuery<TEntity>
    {
        Expression<Func<TEntity, bool>> Predicate { get; }
    }
}

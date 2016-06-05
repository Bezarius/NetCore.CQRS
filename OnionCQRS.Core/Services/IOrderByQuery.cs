using System;
using System.Linq;

namespace OnionCQRS.Core.Services
{
    public interface IOrderByQuery<TEntity>
    {
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> OrderBy { get; }
    }
}

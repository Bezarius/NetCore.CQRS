using System;
using System.Linq.Expressions;

namespace OnionCQRS.Core.Services
{
    public interface IIncludeQuery<TEntity>
    {
        Expression<Func<TEntity, object>>[] IncludeProperties { get; }
    }
}

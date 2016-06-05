using System.Data;
using DbContextScope.UnitOfWork.Core.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace DbContextScope.UnitOfWork.EfCore
{
    public interface IEntityFrameworkUnitOfWork<TContext> : IUnitOfWork where TContext : class
    {
        IDbContextTransaction BeginTransaction();

        IDbContextTransaction BeginTransaction(IsolationLevel isolationLevel);
    }
}

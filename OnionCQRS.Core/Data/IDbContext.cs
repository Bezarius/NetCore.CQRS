using Microsoft.EntityFrameworkCore;

namespace OnionCQRS.Core.Data
{
    public interface IDbContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}

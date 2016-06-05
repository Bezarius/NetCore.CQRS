using Microsoft.EntityFrameworkCore;
using OnionCQRS.Core.DomainModels;

namespace OnionCQRS.Core.Data
{
    public interface ICompanyDbContext : IDbContext
    {
        DbSet<Employee> Employees { get; }
        DbSet<Departament> Departaments { get; }
    }
}

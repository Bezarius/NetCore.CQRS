using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OnionCQRS.Core.Data;
using OnionCQRS.Core.DomainModels;
using OnionCQRS.Data.Helper;

namespace OnionCQRS.Data
{
    //http://benjii.me/2016/05/dotnet-ef-migrations-for-asp-net-core/
    public class CompanyDbContext : DbContext, ICompanyDbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Departament> Departaments { get; set; }

        protected IConfiguration Config { get; }

        static CompanyDbContext()
        {
            //ObjectMapper.MapObjects();
        }

        public CompanyDbContext(IConfiguration config)
        {
            Config = config;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.AddEntityConfigurationsFromAssembly(Assembly.Load("OnionCQRS.Data"));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Config["Data:Connection"]);
        }
    }
}

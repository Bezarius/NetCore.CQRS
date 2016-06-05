using System.Reflection;
using Autofac;
using DbContextScope.EfCore.Implementations;
using DbContextScope.EfCore.Interfaces;
using OnionCQRS.Bootstrapper;
using OnionCQRS.Core.Data;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(IocConfig), nameof(IocConfig.RegisterDependencies))]

namespace OnionCQRS.Bootstrapper
{
    public class IocConfig
    {
        public static void RegisterDependencies()
        {
            DbContextScopeExtensionConfig.Setup();

            var builder = new ContainerBuilder();

            builder.RegisterType<CompanyDbContext>().As<ICompanyDbContext>().InstancePerRequest();
            
            builder.RegisterType<DbContextScopeFactory>().As<IDbContextScopeFactory>().SingleInstance();
            
            builder.Register(b => NLogLogger.Instance).SingleInstance();
            
            // Registers our IMediator (abstraction for observer pattern, which lets us use CQRS)
            builder.RegisterModule(new MediatorModule(
                Assembly.Load("OnionCQRS.Services")));

            // Registers our Fluent Validations that we use on our Models
            builder.RegisterModule(new FluentValidationModule(
                Assembly.Load("OnionCQRS.MyODataApi"),
                Assembly.Load("OnionCQRS.Services")));

            // Registers our AutoMapper Profiles
            builder.RegisterModule(new AutoMapperModule(
                Assembly.Load("OnionCQRS.MyODataApi"),
                Assembly.Load("OnionCQRS.Services")));

            var container = builder.Build();
        }
    }
}

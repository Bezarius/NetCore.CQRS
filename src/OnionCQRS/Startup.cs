using System;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using DbContextScope.EfCore.Implementations;
using DbContextScope.EfCore.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OnionCQRS.Bootstrapper;
using OnionCQRS.Data;
using OnionCQRS.Infrastructure.Logging;

namespace OnionCQRS
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<CompanyDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc();

            // Add Autofac
            var containerBuilder = new ContainerBuilder();

            DbContextScopeExtensionConfig.Setup();

            //containerBuilder.RegisterType<CompanyDbContext>().As<ICompanyDbContext>().InstancePerRequest();

            containerBuilder.RegisterType<DbContextScopeFactory>()
                .As<IDbContextScopeFactory>().SingleInstance();

            containerBuilder.Register(b => NLogLogger.Instance).SingleInstance();

            // Registers our IMediator (abstraction for observer pattern, which lets us use CQRS)
            containerBuilder.RegisterModule(new MediatorModule(
                Assembly.Load("OnionCQRS.Services")));

            // Registers our Fluent Validations that we use on our Models
            containerBuilder.RegisterModule(new FluentValidationModule(
                Assembly.Load("OnionCQRS.Services")));

            // Registers our AutoMapper Profiles
            containerBuilder.RegisterModule(new AutoMapperModule(
                Assembly.Load("OnionCQRS.Services")));

            containerBuilder.Populate(services);

            var container = containerBuilder.Build();

            return container.Resolve<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

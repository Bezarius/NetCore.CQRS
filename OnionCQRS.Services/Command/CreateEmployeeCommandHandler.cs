using System;
using DbContextScope.EfCore.Interfaces;
using MediatR;
using OnionCQRS.Core.Command;
using OnionCQRS.Core.DomainModels;
using OnionCQRS.Core.Data;
using OnionCQRS.Core.Extensions;

namespace OnionCQRS.Services.Command
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Employee>
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;

        public CreateEmployeeCommandHandler(IDbContextScopeFactory dbContextScopeFactory)
        {
            if (dbContextScopeFactory == null)
                throw new ArgumentNullException(nameof(dbContextScopeFactory));

            _dbContextScopeFactory = dbContextScopeFactory;
        }

        public Employee Handle(CreateEmployeeCommand command)
        {
            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                // Gets our context from our context scope
                var dbCtx = dbContextScope.DbContexts.GetByInterface<ICompanyDbContext>();

                // Map our command to a new employee entity. We purposely don't use automapping for this. We want to control our mapping in a 1 to 1 manner
                var domainModel = new Employee
                {
                    FirstName = command.FirstName,
                    LastName = command.LastName
                };

                dbCtx.Employees.Add(domainModel);

                dbContextScope.SaveChanges();

                // This employee will have the Id field populated.
                return domainModel;
            }
        }
    }
}

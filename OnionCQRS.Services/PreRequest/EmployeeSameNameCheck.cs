using System;
using System.Linq;
using DbContextScope.EfCore.Interfaces;
using OnionCQRS.Core.Command;
using OnionCQRS.Core.Data;
using OnionCQRS.Core.Extensions;
using OnionCQRS.Core.Services;

namespace OnionCQRS.Services.PreRequest
{
    // this is just pre-request check demo
    public class EmployeeSameNameCheck : IPreRequestHandler<CreateEmployeeCommand>
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;

        public EmployeeSameNameCheck(IDbContextScopeFactory dbContextScopeFactory)
        {
            if (dbContextScopeFactory == null)
                throw new ArgumentNullException(nameof(dbContextScopeFactory));

            _dbContextScopeFactory = dbContextScopeFactory;
        }

        public void Handle(CreateEmployeeCommand request)
        {
            using (var dbContextScope = _dbContextScopeFactory.CreateReadOnly())
            {
                // Gets our context from our context scope
                var dbCtx = dbContextScope.DbContexts.GetByInterface<ICompanyDbContext>();

                // Find if employee with same first and last name already exists
                var count = dbCtx.Employees.Count(x => x.FirstName == request.FirstName && x.LastName == request.LastName);

                if (count > 0)
                    throw new InvalidOperationException("A Employee with that last and first name already exists");
            }
        }
    }
}

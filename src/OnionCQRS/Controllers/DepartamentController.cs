using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnionCQRS.Core.Data;

namespace OnionCQRS.Controllers
{
    public class DepartamentController : Controller
    {
        private readonly ICompanyDbContext _dbContext;
        private readonly IMediator _mediator;

        public DepartamentController(ICompanyDbContext dbContext, IMediator mediator)
        {
            if (dbContext == null)
                throw new ArgumentNullException(nameof(dbContext));
            _dbContext = dbContext;

            if (mediator == null)
                throw new ArgumentNullException(nameof(mediator));
            _mediator = mediator;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
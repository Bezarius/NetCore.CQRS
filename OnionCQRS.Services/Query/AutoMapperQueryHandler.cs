using System;
using System.Collections.Generic;
using System.Linq;
using DbContextScope.EfCore.Interfaces;
using MediatR;
using OnionCQRS.Core.Data;
using OnionCQRS.Core.Extensions;
using OnionCQRS.Core.Query;
using AutoMapper.QueryableExtensions;

namespace OnionCQRS.Services.Query
{
    public class AutoMapperQueryHandler<TSrcEntity, TDestModel> : IRequestHandler<AutoMapperQuery<TSrcEntity, TDestModel>, IEnumerable<TDestModel>>
        where TSrcEntity : class
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;

        public AutoMapperQueryHandler(IDbContextScopeFactory dbContextScopeFactory)
        {
            if (dbContextScopeFactory == null)
                throw new ArgumentNullException(nameof(dbContextScopeFactory));

            _dbContextScopeFactory = dbContextScopeFactory;
        }

        public IEnumerable<TDestModel> Handle(AutoMapperQuery<TSrcEntity, TDestModel> args)
        {
            using (var dbContextScope = _dbContextScopeFactory.CreateReadOnly())
            {
                // Gets our context from our context scope
                var dbCtx = dbContextScope.DbContexts.GetByInterface<ICompanyDbContext>();

                IQueryable<TSrcEntity> srcEntities = dbCtx.Set<TSrcEntity>();

                srcEntities = srcEntities.Where(args);
                IQueryable<TDestModel> destEntities = srcEntities.ProjectTo<TDestModel>();
                destEntities = destEntities.OrderBy(args);
                return destEntities.Take(args.PageSize).ToList();
            }
        }
    }
}

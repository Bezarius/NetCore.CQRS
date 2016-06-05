using OnionCQRS.Core.Data;
using OnionCQRS.Core.Extensions;
using OnionCQRS.Data;

namespace OnionCQRS.Bootstrapper
{
    public static class DbContextScopeExtensionConfig
    {
        public static void Setup()
        {
            DbContextScopeExtensions.GetDbContextFromCollection = (collection, type) =>
            {
                if(type == typeof(ICompanyDbContext))
                    return collection.Get<CompanyDbContext>();
                return null;
            };

            DbContextScopeExtensions.GetDbContextFromLocator = (locator, type) =>
            {
                if (type == typeof(ICompanyDbContext))
                    return locator.Get<CompanyDbContext>();
                return null;

            };
        }
    }
}

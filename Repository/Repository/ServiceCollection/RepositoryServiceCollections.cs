using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository.Interfaces;
using Repository.Model;

namespace Repository.ServiceCollection
{
    public static class RepositoryServiceCollections
    {
        /// <summary>
        /// Sets repository related services.
        /// </summary>
        /// <typeparam name="TDbContext">DbContext used.</typeparam>
        /// <param name="services">EF Core service collections</param>
        /// <returns>Related repository based services.</returns>
        public static IServiceCollection AddServices<TDbContext>(this IServiceCollection services)
            where TDbContext : DbContext
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<DbContext, TDbContext>();
            return services;
        }
    }
}

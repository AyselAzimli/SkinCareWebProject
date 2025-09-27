using ECommerce.DAL.DataContext;
using ECommerce.DAL.Repositories;
using ECommerce.DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.DAL
{
    public static class DataAccessLayerServiceRegistration
    {
        public static IServiceCollection AddDataAccessLayerServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), options =>
                {
                    options.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                }));

            services.AddScoped<DataInitializer>();

            services.AddScoped(typeof(IRepository<>), typeof(EfCoreRepository<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            return services;
        }
    }
}

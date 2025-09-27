
using ECommerce.BLL.Mapping;
using ECommerce.BLL.Services;
using ECommerce.BLL.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.BLL
{
    public static class BusinessLogicLayerServiceRegistration
    {
        public static IServiceCollection AddBusinessLogicLayerServices(this IServiceCollection services)
        {
            services.AddAutoMapper(confg => confg.AddProfile<MappingProfile>());

            services.AddScoped(typeof(ICrudService<,,,>), typeof(CrudManager<,,,>));
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<IHomeService, HomeManager>();
            //services.AddScoped<FileService>();
            //services.AddScoped<BasketManager>();

            return services;
        }
    }
}

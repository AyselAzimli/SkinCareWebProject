
using ECommerce.BLL.Mapping;
using ECommerce.BLL.Services;
using ECommerce.BLL.Services.Contracts;
using ECommerce.Web.Models;
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
            services.AddScoped<IBrandService, BrandManager>(); // Add this line
            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<IHomeService, HomeManager>();
            services.AddScoped<IDetailsService, DetailsManager>();

            services.AddScoped<IShopService, ShopManager>();
            services.AddScoped<IHeaderService, HeaderManager>();
            services.AddScoped<IFooterService, FooterManager>();
            services.AddScoped<FileService>();
            services.AddScoped<IBioService, BioManager>();
            services.AddScoped<ISocialService, SocialManager>();
            services.AddScoped<ILanguageService, LanguageManager>();
            services.AddScoped<ICurrencyService, CurrencyManager>();
            // services.AddScoped<ISliderService,SliderManager>();
            services.AddScoped<IAddressService, AddressManager>();
            services.AddScoped<IOrderService, OrderManager>();
            services.AddScoped<IOrderItemService, OrderItemManager>();

            services.AddScoped<IProductVariantService, ProductVariantManager>();
            services.AddScoped<BasketManager>();


            return services;
        }
    }
}

using ECommerce.DAL;
using ECommerce.BLL;
using ECommerce.DAL.DataContext;
using ECommerce.DAL.DataContext.Entities;
using Microsoft.AspNetCore.Identity;
using Mailing.MailKitImplementations;
using Mailing;
using ECommerce.BLL.Services.Contracts;
using ECommerce.BLL.Services;

namespace ECommerce.MVC
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDataAccessLayerServices(builder.Configuration); //dataAccesLayer ucundu,(dependencies-->DAL)
            builder.Services.AddBusinessLogicLayerServices(); //initializeri register edir


            builder.Services.AddHttpContextAccessor();

            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;

                //options.User.RequireUniqueEmail = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 3;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();





            builder.Services.AddScoped<DataInitializer>();
            builder.Services.AddScoped<IMailService, MailKitMailService>();

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddScoped<IProductService, ProductManager>();
            builder.Services.AddScoped<IProductVariantService, ProductVariantManager>();
            builder.Services.AddScoped<ICategoryService, CategoryManager>();
            builder.Services.AddScoped<IBrandService, BrandManager>();

            builder.Services.AddScoped<BasketManager>();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }










            using (var scope = app.Services.CreateScope()) //initializeri serviceproviderden goturur
            {
                var dataInitializer = scope.ServiceProvider.GetRequiredService<DataInitializer>();
                await dataInitializer.InitializeAsync();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            await app.RunAsync();
        }
    }
}

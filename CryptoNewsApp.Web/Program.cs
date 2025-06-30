using CryptoNewsApp.Application.Interfaces;
using CryptoNewsApp.Application.Services;
using CryptoNewsApp.Core.Entities;
using CryptoNewsApp.Core.Repositories;
using CryptoNewsApp.Core.UnitOfWork;
using CryptoNewsApp.Infrastructure.Data;
using CryptoNewsApp.Infrastructure.ExternalServices;
using CryptoNewsApp.Infrastructure.Repositories;
using CryptoNewsApp.Infrastructure.Services;
using CryptoNewsApp.Infrastructure.UnitOfWorks;
using CryptoNewsApp.Web.Seed;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CryptoNewsApp.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();


            //DbContext
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddScoped<INewsRepository, NewsRepository>();
            builder.Services.AddScoped<INewsService, NewsService>();


            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();

            builder.Services.AddHttpClient<INewsApiClient, CryptoNewsApiClient>();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddSingleton<IPasswordGenerator, PasswordGenerator>();



            //UnitOfWork
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();



            //Cookie ayarlarý
            //kullanýcý giriþ yaptýðýnda sistemin onu otomatik olarak nereye yönlendireceðini belirler.
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Auth/Login";
                options.LogoutPath = "/Auth/Logout";
                options.AccessDeniedPath = "/Auth/AccessDenied";
                options.ExpireTimeSpan = TimeSpan.FromDays(14);
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            using (var scope = app.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                await SeedData.SeedRolesAsync(serviceProvider); //Rolleri ekle
                await SeedData.SeedAdminUserAsync(serviceProvider); //admin kullanýcyý ekle
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "admin",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Dashboard}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                    name: "author",
                    areaName: "Author",
                    pattern: "Author/{controller=Dashboard}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                    name: "user",
                    areaName: "User",
                    pattern: "User/{controller=Dashboard}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            });



            app.Run();
        }
    }
}
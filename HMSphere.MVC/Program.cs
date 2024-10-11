using HMSphere.Application.Interfaces;
using HMSphere.Application.Services;
using HMSphere.Domain.Entities;
using HMSphere.Infrastructure.DataContext;
using HMSphere.MVC.AutoMapper;
using HMSphere.MVC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using HMSphere.Infrastructure.Repositories;

namespace HMSphere.MVC
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

            builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);

            //Add Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<HmsContext>();

            //Add DbContext
            builder.Services.AddDbContext<HmsContext>(options =>
            options.UseSqlServer(builder.Configuration
            .GetConnectionString("DefaultConnection")));


            //configure  Services
            builder.Services.AddScoped(typeof(IAccountService), typeof(AccountService));
            builder.Services.AddScoped<IUserRoleFactory, UserRoleFactory>();
            builder.Services.AddScoped<IDoctorService, DoctorService>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));



			//seeding Data
			builder.Services.AddScoped<StoredContextSeed>();
           // builder.Services.AddScoped<IdentitySeed>();


            var app = builder.Build();
            //For Seeding Data 
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<HmsContext>();
                var usermanager = services.GetRequiredService<UserManager<ApplicationUser>>();
                await StoredContextSeed.SeedAsync(context);
               // await IdentitySeed.SeedUserAsync(usermanager);
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

			app.UseMiddleware<PerformanceMiddleware>();

			app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Doctor}/{action=Index}/{id?}");

            app.Run();
        }
    }
}

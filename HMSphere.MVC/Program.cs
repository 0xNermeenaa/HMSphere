using HMSphere.Application.Interfaces;
using HMSphere.Application.Mailing;
using HMSphere.Application.Services;
using HMSphere.Domain.Entities;
using HMSphere.Infrastructure.DataContext;
using HMSphere.Infrastructure.Repositories;
using HMSphere.MVC.AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace HMSphere.MVC
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);

            // Configure Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<HmsContext>()
                .AddDefaultTokenProviders();

            // CORS policy configuration
            builder.Services.AddCors(corsOptions =>
            {
                corsOptions.AddPolicy("MyPolicy",
                    corsPolicyBuilder => corsPolicyBuilder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });

            // Database Context
            builder.Services.AddDbContext<HmsContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Email configuration
            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("Mailing"));

            // Register application services
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IMailingService, MailingService>();
            builder.Services.AddScoped<IUserRoleFactory, UserRoleFactory>();
            builder.Services.AddScoped<IDoctorService, DoctorService>();
            builder.Services.AddScoped<IStaffService, StaffService>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IAppointmentService, AppointmentService>();
            builder.Services.AddScoped<IShiftService, ShiftService>();
            builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            builder.Services.AddScoped<IPatientService, PatientService>();

            // Localization Configuration
            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[] { new CultureInfo("en-US") };
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            // Authentication and Authorization configuration
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login";
                });

            builder.Services.AddControllersWithViews();
            builder.Services.AddAuthorization();

            var app = builder.Build();

            // Apply localization settings
            var localizationOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>().Value;
            app.UseRequestLocalization(localizationOptions);

            // For Seeding Data
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<HmsContext>();
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                // Optionally seed database
                // await StoredContextSeed.SeedUserAsync(userManager, context);
            }

            // Configure HTTP request pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors("MyPolicy");
            app.UseRouting();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<PerformanceMiddleware>();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Register}/{id?}");

            app.Run();
        }
    }
}

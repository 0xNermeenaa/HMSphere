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
using HMSphere.Application.Helpers;
namespace HMSphere.MVC
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
			builder.Services.AddHttpContextAccessor();
			builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);

			//Add Identity
			builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<HmsContext>().AddDefaultTokenProviders();
;

			//Add DbContext
			builder.Services.AddDbContext<HmsContext>(options =>
			options.UseSqlServer(builder.Configuration
			.GetConnectionString("DefaultConnection")));

			//configure  Services
			builder.Services.AddScoped(typeof(IAccountService), typeof(AccountService));
			builder.Services.AddScoped<IUserHelpers, UserHelpers>();
			builder.Services.AddScoped<IUserRoleFactory, UserRoleFactory>();
			builder.Services.AddScoped<IDoctorService, DoctorService>();
			builder.Services.AddScoped<IDepartmentService, DepartmentService>();
			builder.Services.AddScoped<IAppointmentService, AppointmentsService>();
			builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));



			//seeding Data
			builder.Services.AddScoped<StoredContextSeed>();
			// builder.Services.AddScoped<IdentitySeed>();

			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"])),
					ValidateIssuer = true,
					ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
					ValidateAudience = true,
					ValidAudience = builder.Configuration["JWT:ValidAudience"],
					ClockSkew = TimeSpan.Zero // Optional: reduce the default clock skew
				};
			});

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
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseMiddleware<PerformanceMiddleware>();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Account}/{action=Login}/{id?}");

			app.Run();
		}
	}
}

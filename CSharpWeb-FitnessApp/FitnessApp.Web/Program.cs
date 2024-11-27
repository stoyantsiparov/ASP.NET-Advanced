using FitnessApp.Data;
using FitnessApp.Services.Data;
using FitnessApp.Services.Data.Contracts;
using FitnessApp.Services.Mapping;
using FitnessApp.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Web
{
	public class Program
	{
		public static void Main(string[] args)
		{
			WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
			builder.Services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(connectionString));
			builder.Services.AddDatabaseDeveloperPageExceptionFilter();

			// За сега ги оставям така за да мога да се логвам по лесно
			builder.Services.AddDefaultIdentity<IdentityUser>(options =>
				{
					options.SignIn.RequireConfirmedAccount = false;
					options.Password.RequireDigit = false;
					options.Password.RequireUppercase = false;
					options.Password.RequireNonAlphanumeric = false;
				})
				.AddEntityFrameworkStores<ApplicationDbContext>();

			builder.Services.AddControllersWithViews();
            // Add Razor Pages
            builder.Services.AddScoped<ISpaProcedureService, SpaProcedureService>();
            builder.Services.AddScoped<IFitnessEventService, FitnessEventService>();
			builder.Services.AddScoped<IClassService, ClassService>();
			builder.Services.AddScoped<IMembershipTypeService, MembershipTypeService>();

			//TODO: Add repositories and implement them
			//builder.Services.AddScoped<IRepository<Class, int>, BaseRepository<Class, int>>();
			//builder.Services.AddScoped<IRepository<ClassRegistration, int>, BaseRepository<ClassRegistration, int>>();
			//builder.Services.AddScoped<IRepository<EventRegistration, int>, BaseRepository<EventRegistration, int>>();
			//builder.Services.AddScoped<IRepository<FitnessEventViewModel, int>, BaseRepository<FitnessEventViewModel, int>>();
			//builder.Services.AddScoped<IRepository<Instructor, int>, BaseRepository<Instructor, int>>();
			//builder.Services.AddScoped<IRepository<Member, int>, BaseRepository<Member, int>>();
			//builder.Services.AddScoped<IRepository<MembershipType, int>, BaseRepository<MembershipType, int>>();
			//builder.Services.AddScoped<IRepository<SpaProcedure, int>, BaseRepository<SpaProcedure, int>>();
			//builder.Services.AddScoped<IRepository<SpaRegistration, int>, BaseRepository<SpaRegistration, int>>();

			WebApplication app = builder.Build();

			//TODO: Add AutoMapper
			// Тук вместо ErrorViewModel трябва да сложа моите си модели които ще ползвам
			AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).Assembly);

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseMigrationsEndPoint();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");
			app.MapRazorPages();

			app.Run();
		}
	}
}

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static FitnessApp.Common.ApplicationsConstants;

namespace FitnessApp.Data.Configuration;

public class RolesSeeder
{
	public static void SeedRoles(IServiceProvider serviceProvider)
	{
		var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

		string[] roles = { AdminRole, MemberRole, UserRole };

		foreach (var role in roles)
		{
			var roleExist = roleManager.RoleExistsAsync(role).GetAwaiter().GetResult();
			if (!roleExist)
			{
				var result = roleManager.CreateAsync(new IdentityRole { Name = role }).GetAwaiter().GetResult();
				if (!result.Succeeded)
				{
					throw new Exception($"Failed to create role: {role}");
				}
			}
		}
	}

	public static void AssignAdminRole(IServiceProvider serviceProvider)
	{
		var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
		var configuration = serviceProvider.GetRequiredService<IConfiguration>();

		string adminEmail = configuration["AdminSettings:Username"];
		string adminPassword = configuration["AdminSettings:Password"];

		var existingAdmin = userManager.FindByEmailAsync(adminEmail!).GetAwaiter().GetResult();

		if (existingAdmin == null)
		{
			var adminUser = new IdentityUser
			{
				UserName = adminEmail,
				Email = adminEmail
			};

			var result = userManager.CreateAsync(adminUser, adminPassword!).GetAwaiter().GetResult();

			if (result.Succeeded)
			{
				userManager.AddToRoleAsync(adminUser, AdminRole).GetAwaiter().GetResult();
			}
			else
			{
				throw new Exception("Failed to create admin user");
			}
		}
	}
}
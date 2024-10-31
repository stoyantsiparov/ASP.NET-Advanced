using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace FitnessApp.Web.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
	public static void RegisterRepositories(this IServiceCollection services)
	{
		Assembly.GetExecutingAssembly()
			.GetTypes()
			.Where(t => t.IsClass && !t.IsAbstract && !t.IsInterface && t.Name.EndsWith("Repository"))
			.ToList()
			.ForEach(repository => services.AddScoped(repository.GetInterfaces().First(), repository));
	}
}
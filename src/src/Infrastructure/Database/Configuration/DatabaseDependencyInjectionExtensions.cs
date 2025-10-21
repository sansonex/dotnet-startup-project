namespace TServiceNameT.Infrastructure.Database.Configuration
{
	using System.Diagnostics.CodeAnalysis;
	using System.Reflection;

	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.DependencyInjection;

	using TServiceNameT.Domain.Interfaces;
	using TServiceNameT.Domain.Interfaces.Repositories;
	using TServiceNameT.Infrastructure.Extensions;
	using TServiceNameT.Infrastructure.Settings;

	[ExcludeFromCodeCoverage]
	public static class DataDependencyInjectionExtensions
	{
		public static IServiceCollection ConfigureDatabase(this IServiceCollection services, Assembly assembly)
		{
			services
				.AddDbContext<DataContext>(
					(serviceProvider, optionsBuilder) =>
					{
						var settings = serviceProvider.GetRequiredService<MySqlSettings>();
						optionsBuilder.UseMySql(settings.ConnectionString, ServerVersion.AutoDetect(settings.ConnectionString));
					})
				.AddTransient<IUnitOfWork, UnitOfWork>()
				.ScanAndRegisterImplementedTypes(
					assemblies: [assembly],
					types: [typeof(IRepository)],
					lifetime: ServiceLifetime.Scoped);

			return services;
		}
	}
}
namespace Globo.TServiceNameT.Infrastructure.Database.Configuration
{
	using System.Diagnostics.CodeAnalysis;
	using System.Reflection;

	using Globo.TServiceNameT.Domain.Interfaces;
	using Globo.TServiceNameT.Domain.Interfaces.Repositories;
	using Globo.TServiceNameT.Infrastructure.Extensions;
	using Globo.TServiceNameT.Infrastructure.Settings;

	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.DependencyInjection;

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
					lifetime: ServiceLifetime.Transient);

			return services;
		}
	}
}
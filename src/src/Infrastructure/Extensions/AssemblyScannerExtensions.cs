namespace TServiceNameT.Infrastructure.Extensions
{
	using System;
	using System.Reflection;

	using Microsoft.Extensions.DependencyInjection;

	internal static class AssemblyScannerExtensions
	{
		public static IServiceCollection ScanAndRegisterImplementedTypes(
			this IServiceCollection services,
			Assembly[] assemblies,
			Type[] types,
			ServiceLifetime lifetime)
		{
			return services.Scan(config =>
				config.FromAssemblies(assemblies)
					.AddClasses(filter => filter.AssignableToAny(types))
					.AsImplementedInterfaces()
					.WithLifetime(lifetime));
		}
	}
}
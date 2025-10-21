namespace Globo.TServiceNameT.Infrastructure
{
	using System;
	using System.Reflection;

	using Globo.TServiceNameT.Domain.Interfaces.Services;
	using Globo.TServiceNameT.Infrastructure.Database.Configuration;
	using Globo.TServiceNameT.Infrastructure.Extensions;
	using Globo.TServiceNameT.Infrastructure.Mediator;
	using Globo.TServiceNameT.Infrastructure.Settings;

	using Microsoft.Extensions.DependencyInjection;

	public static class InfrastructureInitializer
	{
		private static readonly Assembly DomainAssembly;
		private static readonly Assembly InfrastructureAssembly;

		static InfrastructureInitializer()
		{
			DomainAssembly = AppDomain.CurrentDomain.Load("Globo.TServiceNameT.Domain");
			InfrastructureAssembly = AppDomain.CurrentDomain.Load("Globo.TServiceNameT.Infrastructure");
		}

		public static IServiceCollection ConfigureApplicationInfrastructure(this IServiceCollection services)
		{
			return services
				.ConfigureSettings()
				.ConfigureMediator(DomainAssembly, InfrastructureAssembly)
				.ConfigureDatabase(InfrastructureAssembly)
				.ScanAndRegisterImplementedTypes(
					assemblies: [DomainAssembly, InfrastructureAssembly],
					types: [typeof(IService)],
					lifetime: ServiceLifetime.Transient);
		}
	}
}
namespace TServiceNameT.Infrastructure
{
	using System;
	using System.Reflection;

	using Microsoft.Extensions.DependencyInjection;

	using TServiceNameT.Domain.Interfaces.Services;
	using TServiceNameT.Infrastructure.Database.Configuration;
	using TServiceNameT.Infrastructure.Extensions;
	using TServiceNameT.Infrastructure.Mediator;
	using TServiceNameT.Infrastructure.Settings;

	public static class InfrastructureInitializer
	{
		private static readonly Assembly DomainAssembly;
		private static readonly Assembly InfrastructureAssembly;

		static InfrastructureInitializer()
		{
			DomainAssembly = AppDomain.CurrentDomain.Load("TServiceNameT.Domain");
			InfrastructureAssembly = AppDomain.CurrentDomain.Load("TServiceNameT.Infrastructure");
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
namespace Globo.TServiceNameT.Infrastructure.Mediator
{
	using System.Reflection;

	using Microsoft.Extensions.DependencyInjection;

	internal static class MediatorDependencyInjectionExtensions
	{
		public static IServiceCollection ConfigureMediator(this IServiceCollection services, params Assembly[] assemblies)
		{
			return services.AddMediatR(x => { x.RegisterServicesFromAssemblies(assemblies); });
		}
	}
}
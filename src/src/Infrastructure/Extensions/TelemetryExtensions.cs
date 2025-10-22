namespace TServiceNameT.Infrastructure.Extensions
{
	using Microsoft.Extensions.DependencyInjection;

	using OpenTelemetry.Metrics;
	using OpenTelemetry.Resources;
	using OpenTelemetry.Trace;

	public static class TelemetryExtensions
	{
		public static IServiceCollection ConfigureOpenTelemetry(this IServiceCollection serviceCollection)
		{
			serviceCollection
				.AddOpenTelemetry()
				.ConfigureResource(x =>
				{
					x.AddService("TServiceNameT", serviceVersion: "1.0.0");
				})
				.WithMetrics(x =>
				{
					x.AddHttpClientInstrumentation();
					x.AddAspNetCoreInstrumentation();
					x.AddRuntimeInstrumentation();
					x.AddOtlpExporter();
				})
				.WithTracing(x =>
				{
					x.AddHttpClientInstrumentation();
					x.AddAspNetCoreInstrumentation();
					x.AddEntityFrameworkCoreInstrumentation();
					x.AddOtlpExporter();
				});

			return serviceCollection;
		}
	}
}
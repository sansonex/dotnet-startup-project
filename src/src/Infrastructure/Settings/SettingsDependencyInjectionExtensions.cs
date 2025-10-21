namespace TServiceNameT.Infrastructure.Settings
{
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	public static class SettingsDependencyInjectionExtensions
	{
		private const string DependenciesSection = "Dependencies";

		public static IServiceCollection ConfigureSettings(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddSingleton<MySqlSettings>(
				provider => provider
					.GetRequiredService<IConfiguration>()
					.GetSection(DependenciesSection)
					.GetSection("MySql")
					.Get<MySqlSettings>());

			return serviceCollection;
		}
	}
}
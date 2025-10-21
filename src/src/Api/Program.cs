namespace Globo.TServiceNameT.Api
{
	using System.Text.Json;
	using System.Text.Json.Serialization;

	using Globo.TServiceNameT.Api.Extensions;
	using Globo.TServiceNameT.Infrastructure;

	using Microsoft.AspNetCore.Builder;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Hosting;

	using Serilog;

	public static class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
			{
				loggerConfiguration
					.ReadFrom.Configuration(hostingContext.Configuration)
					.Enrich.FromLogContext();
			});

			// Add services to the container.
			builder.Services.AddControllers().AddJsonOptions(x =>
			{
				x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
				x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
				x.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
				x.JsonSerializerOptions.WriteIndented = true;
			});

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddSwagger();

			builder.Services.ConfigureApplicationInfrastructure();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwaggerConfiguration();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}
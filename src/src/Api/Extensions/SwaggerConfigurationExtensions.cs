namespace TServiceNameT.Api.Extensions
{
	using System;
	using System.Diagnostics.CodeAnalysis;
	using System.IO;

	using Microsoft.AspNetCore.Builder;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.OpenApi.Models;

	using Swashbuckle.AspNetCore.SwaggerGen;

	[ExcludeFromCodeCoverage]
	public static class SwaggerConfiguratorExtensions
	{
		public static IServiceCollection AddSwagger(this IServiceCollection swaggerConfigurator)
		{
			swaggerConfigurator.AddEndpointsApiExplorer();
			swaggerConfigurator.AddSwaggerGen(x =>
			{
				x.SwaggerDoc("v0",
					new OpenApiInfo
					{
						Title = "API Endpoints",
						Version = "v0",
					});

				x.SwaggerDoc("v1",
					new OpenApiInfo
					{
						Title = "API Endpoints",
						Version = "v1",
					});

				x.OperationFilter<OpenApiParameterIgnoreFilter>();
				x.CustomSchemaIds(x => x.FullName);
				x.DocInclusionPredicate((docName, apiDesc) => apiDesc.RelativePath.Contains(docName));
				x.IncludeXmlComments();
			});

			return swaggerConfigurator;
		}

		public static void UseSwaggerConfiguration(this IApplicationBuilder app)
		{
			app.UseSwagger(x => x.RouteTemplate = "api/swagger/{documentName}/swagger.json");
			app.UseSwaggerUI(options =>
			{
				options.RoutePrefix = "api/swagger";
				options.SwaggerEndpoint("/api/swagger/v1/swagger.json", "API v1 Endpoints");
				options.SwaggerEndpoint("/api/swagger/v0/swagger.json", "API - Maintenance");
			});
		}

		private static void IncludeXmlComments(this SwaggerGenOptions options)
		{
			var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

			foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				var name = $"{assembly.GetName().Name}.xml";
				var fullPath = Path.Combine(baseDirectory, name);

				if (File.Exists(fullPath))
				{
					options.IncludeXmlComments(fullPath);
				}
			}
		}
	}
}
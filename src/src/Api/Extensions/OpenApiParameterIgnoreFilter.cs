namespace Globo.TServiceNameT.Api.Extensions
{
	using System;
	using System.Diagnostics.CodeAnalysis;
	using System.Linq;

	using Microsoft.OpenApi.Models;

	using Swashbuckle.AspNetCore.SwaggerGen;

	[ExcludeFromCodeCoverage]
	public class OpenApiParameterIgnoreFilter : IOperationFilter
	{
		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			if (operation == null || context?.ApiDescription?.ParameterDescriptions == null)
				return;

			var parametersToObsolete = context.ApiDescription.ParameterDescriptions
				.Where(ParameterHasObsoleteAttribute)
				.ToList();

			foreach (var parameterToObsolete in parametersToObsolete)
			{
				var parameter = operation.Parameters.FirstOrDefault(parameter =>
					string.Equals(parameter.Name, parameterToObsolete.Name, StringComparison.Ordinal));

				if (parameter is not null)
				{
					parameter.Deprecated = true;
				}
			}
		}

		private static bool ParameterHasObsoleteAttribute(
			Microsoft.AspNetCore.Mvc.ApiExplorer.ApiParameterDescription parameterDescription)
		{
			if (parameterDescription.ModelMetadata is Microsoft.AspNetCore.Mvc.ModelBinding.Metadata.DefaultModelMetadata
			    metadata)
			{
				return
					(metadata.Attributes.Attributes?.Any(attribute => attribute is ObsoleteAttribute) ??
					 false);
			}

			return false;
		}
	}
}
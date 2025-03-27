using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NZWalks.API
{
	public class ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) : IConfigureNamedOptions<SwaggerGenOptions>
	{
		public void Configure(string? name, SwaggerGenOptions options)
		{
			foreach (var description in provider.ApiVersionDescriptions)
			{
				options.SwaggerDoc(description.GroupName, new OpenApiInfo
				{
					Title = "NZWalks API",
					Version = description.ApiVersion.ToString()
				});
			}
		}

		public void Configure(SwaggerGenOptions options)
		{
			Configure(string.Empty, options);
		}
	}
}

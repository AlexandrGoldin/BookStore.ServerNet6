using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace WebApi
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                var apiVersion = description.ApiVersion.ToString();
                options.SwaggerDoc(description.GroupName,
                    new OpenApiInfo
                    {
                        Version = apiVersion,
                        Title = $"BookStore.ServerNet6.WebApi {apiVersion}",
                        Description =
                            "A demo eCommerce BookStore.ServerNet6 backend application" +
                            " for BookStore.Client frontend application",                      
                        Contact = new OpenApiContact
                        {
                            Name = " GitHub",
                            Email = string.Empty,
                            Url =
                                new Uri("https://github.com/AlexandrGoldin")
                        }                      
                    });

                options.CustomOperationIds(apiDescription =>
                   apiDescription.TryGetMethodInfo(out MethodInfo methodInfo)
                       ? methodInfo.Name
                       : null);
            }
        }
    }
}

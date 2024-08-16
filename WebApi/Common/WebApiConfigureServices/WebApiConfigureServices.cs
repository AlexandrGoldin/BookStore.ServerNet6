using FluentValidation;
using MediatR;
using System.Reflection;
using WebApi.Common.Behaviors;

namespace WebApi.Common.WebApiConfigureServices
{
    public static class WebApiConfigureServices
    {
        public static IServiceCollection AddWebApiServices(
           this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssemblies(typeof(WebApiConfigureServices).Assembly));
            services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });
            services.AddTransient(typeof(IPipelineBehavior<,>),
                typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>),
                typeof(LoggingBehavior<,>));
            return services;
        }
    }
}

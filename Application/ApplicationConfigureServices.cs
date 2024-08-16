using Application.ServicesImplementation;
using Application.ServicesInterfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ApplicationConfigureServices
    {

        public static IServiceCollection AddApplicationServices
            (this IServiceCollection services)
        {          
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<ICartItemService, CartItemService>();
            services.AddTransient<IProductService, ProductService>();

            return services;
        }
    }
}

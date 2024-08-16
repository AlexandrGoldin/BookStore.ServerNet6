using Domain.DapperRepositoriesInterfaces;
using Domain.EfRepositoriesInterfaces;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DapperData;
using Persistence.EfData;

namespace Persistence
{
    public static class PersistenceConfigureServices
    {
        public static IServiceCollection AddPersistenceServices
           (this IServiceCollection services)
        {
            services.AddScoped<IEfUnitOfWork, EfUnitOfWork>();
            services.AddSingleton<IDapperDbConnection, DapperDbConnection>();

            services.AddScoped<IDapperProductRepository, DapperProductRepository>();
            services.AddScoped<IDapperOrderRepository, DapperOrderRepository>();
            services.AddScoped<IDapperCartItemRepository, DapperCartItemRepository>();

            return services;
        }
    }
}

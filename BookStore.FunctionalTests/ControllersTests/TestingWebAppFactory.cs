using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Persistence.EfData;

namespace BookStore.FunctionalTests.ControllersTests
{
    public class TestingWebAppFactory<TEntryPoint> : WebApplicationFactory<Program> 
        where TEntryPoint : Program
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<EfBookStoreContext>));
                if (descriptor != null)
                    services.Remove(descriptor);
                services.AddDbContext<EfBookStoreContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryBookStoreDbTest");
                });
                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                using (var appContext = scope.ServiceProvider.GetRequiredService<EfBookStoreContext>())
                {
                    var serviceProvider=scope.ServiceProvider;

                    try
                    {
                        appContext.Database.EnsureCreated();
                    }
                    catch (Exception ex)
                    {
                        var logger = serviceProvider
                        .GetRequiredService<ILogger<TestingWebAppFactory<TEntryPoint>>>();

                        logger.LogError(ex, "An error occured wile InMemoryBookStoreDbTest initialise" +
                            " in TestingWebAppFactory class");

                        throw;
                    }
                }
            });
        }
    }
}

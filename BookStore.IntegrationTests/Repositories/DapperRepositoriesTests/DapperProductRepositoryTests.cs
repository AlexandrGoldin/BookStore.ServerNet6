using Domain.DapperRepositoriesInterfaces;
using Domain.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using Persistence.DapperData;
using Persistence.EfData;
using Xunit;

namespace BookStore.FunctionalTests.Repositories.DapperRepositoriesTests
{
    public class DapperProductRepositoryTests : IDisposable
    {
        private readonly SqliteConnection _connection;
        private readonly DbContextOptions<EfBookStoreContext> _options;
        private readonly Mock<IDapperDbConnection> _mockDapperDbConnection;

        public DapperProductRepositoryTests()
        {
            _connection = new SqliteConnection("datasource=:memory:");
            _connection.Open();

            _options = new DbContextOptionsBuilder<EfBookStoreContext>()
                .UseSqlite(_connection)
            .Options;

            using (var context = new EfBookStoreContext(_options))
                context.Database.EnsureCreated();

            _mockDapperDbConnection = new();
        }

        [Fact]
        public async Task GetProductByIdAsync_ShouldReturnCorrectProduct()
        {
            // Arrange
            var testProduct = GetTestProduct();

            using (var context = new EfBookStoreContext(_options))
            {
                context.Products!.Add(testProduct);
                await context.SaveChangesAsync();
            }

            _mockDapperDbConnection.Setup(c => c.CreateConnection()).Returns(_connection);

            // Act
            var result = await new DapperProductRepository(_mockDapperDbConnection.Object)
               .GetByIdAsync(testProduct.Id, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Product>(result);
            Assert.True(testProduct.Id == result?.Id && testProduct.Title == result.Title &&
                testProduct.Author == result.Author && testProduct.Image == result.Image &&
                testProduct.Price == result.Price && testProduct.Genre == result.Genre &&
                testProduct.Rating == result.Rating && testProduct.Description == result.Description);
        }

        [Fact]
        public async Task GetProductListAsync_ShouldReturnProductList()
        {
            // Arrange
            var testProduct = GetTestProduct();

            using (var context = new EfBookStoreContext(_options))
            {
                context.Products!.Add(testProduct);
                await context.SaveChangesAsync();
            }

            _mockDapperDbConnection.Setup(c => c.CreateConnection()).Returns(_connection);

            // Act
            var result = await new DapperProductRepository(_mockDapperDbConnection.Object)
               .GetAllAsync(CancellationToken.None);

            // Assert
            Assert.IsType<List<Product>>(result);
            Assert.Equal(1, result.Count(r => r.Id == testProduct.Id));
        }


        [Theory]
        [InlineData(0)]
        [InlineData(999)]
        public async Task GetProductByIdAsync_ProductIdIsZero_Or_OutOfRange_ShouldReturnNull
            (int value)
        { 
            // Arrange
            var testProduct = GetTestProduct();

            using (var context = new EfBookStoreContext(_options))
            {
                context.Products!.Add(testProduct);
                await context.SaveChangesAsync();
            }

            _mockDapperDbConnection.Setup(c => c.CreateConnection()).Returns(_connection);

            // Act
            var result = await new DapperProductRepository(_mockDapperDbConnection.Object)
               .GetByIdAsync(value, CancellationToken.None);

            // Assert          
            Assert.True(result == null);
        }

        private Product GetTestProduct()
        {
            var testProduct = new Product()
            {
                Title = "TextTitle_1",
                Author = "TestAuthor_1",
                Image = "TestImage_1",
                Price = 100.00m,
                Genre = "TestGenre_1",
                Rating = 1,
                Description = "TestDescription_1"
            };
            return testProduct;
        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}

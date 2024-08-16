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
    public class DapperOrderRepositoryTests : IDisposable
    {
        private readonly SqliteConnection _connection;
        private readonly DbContextOptions<EfBookStoreContext> _options;
        private readonly Mock<IDapperDbConnection> _mockDapperDbConnection;

        public DapperOrderRepositoryTests()
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
        public async Task GetOrderByIdAsync_ShouldReturnCorrectOrder()
        {
            // Arrange
            var testOrder = GetTestOrder();
            using (var context = new EfBookStoreContext(_options))
            {
                context.Orders!.Add(testOrder);
                await context.SaveChangesAsync();
            }

            _mockDapperDbConnection.Setup(c => c.CreateConnection()).Returns(_connection);

            // Act
            var result = await new DapperOrderRepository(_mockDapperDbConnection.Object)
               .GetByIdAsync(testOrder.Id, CancellationToken.None);

            // Assert
            Assert.IsType<Order>(result);
            Assert.True(testOrder.Id == result?.Id && testOrder.Email == result.Email &&
                testOrder.Name == result.Name && testOrder.Address == result.Address &&
                testOrder.OrderDate == result.OrderDate && testOrder.Total == result.Total);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(999)]
        public async Task GetOrderByIdAsync_OrderIdIsZero_Or_OutOfRange_ShouldReturnNull
            (int value)       
        {
            // Arrange
            var testOrder = GetTestOrder();
            using (var context = new EfBookStoreContext(_options))
            {
                context.Orders!.Add(testOrder);
                await context.SaveChangesAsync();
            }

            _mockDapperDbConnection.Setup(c => c.CreateConnection()).Returns(_connection);

            // Act
            var result = await new DapperOrderRepository(_mockDapperDbConnection.Object)
               .GetByIdAsync(value, CancellationToken.None);

            // Assert
            Assert.True(result == null);
        }

        private Order GetTestOrder()
        {
            var testOrder = new Order
            {
                Email = "Email.Test_1@mail.com",
                Name = "TestName_1",
                Address = "TestAddress 1",
                OrderDate = DateTime.Now,
                Total = 100.00m,
                CartItems = new List<CartItem>()
                    {
                     new CartItem() {OrderId = 1, ProductId = 3, Count = 2}
                    }
            };
            return testOrder;
        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}

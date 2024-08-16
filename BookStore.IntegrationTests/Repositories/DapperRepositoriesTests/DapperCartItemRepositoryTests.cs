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
    public class DapperCartItemRepositoryTests : IDisposable
    {
        private readonly SqliteConnection _connection;
        private readonly DbContextOptions<EfBookStoreContext> _options;
        private readonly Mock<IDapperDbConnection> _mockDapperDbConnection;

        public DapperCartItemRepositoryTests()
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
        public async Task GetAllCartItemsAsync_ShouldReturnCartItemList()
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
            var result = await new DapperCartItemRepository(_mockDapperDbConnection.Object)
               .GetAllAsync(CancellationToken.None);

            // Assert
            Assert.IsType<List<CartItem>>(result);
            Assert.Equal(testOrder.CartItems!.Count(), result!.Count());

        }

        [Fact]
        public async Task GetCartItemListByOrderIdAsync_ShouldRetrurnCartItemList()
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
            var result = await new DapperCartItemRepository(_mockDapperDbConnection.Object)
               .GetCartItemListByOrderIdAsync(testOrder.Id, CancellationToken.None);

            // Assert
            Assert.IsType<List<CartItem>>(result);
            Assert.Equal(testOrder.CartItems!.Count(), result!.ToList().Count());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(999)]
        public async Task GetCartItemListByOrderIdAsync_OrderIdIsOutOfRange_Or_Zero_ShouldRetrurnCartItemsCountIsZero
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
            var result = await new DapperCartItemRepository(_mockDapperDbConnection.Object)
               .GetCartItemListByOrderIdAsync(value, CancellationToken.None);

            // Assert
            Assert.True(result?.Count() == 0);
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
                     new CartItem() {ProductId = 2, Count = 1},
                     new CartItem() {ProductId = 1, Count = 3},
                     new CartItem() {ProductId = 3, Count = 2}
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

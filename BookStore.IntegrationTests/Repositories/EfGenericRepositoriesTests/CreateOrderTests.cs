using Domain.EfRepositoriesInterfaces;
using Domain.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Persistence.EfData;
using Xunit;

namespace BookStore.FunctionalTests.Repositories.EfGenericRepositoriesTests
{
    public class CreateOrderTests : IDisposable
    {
        private readonly SqliteConnection _connection;
        private readonly IEfUnitOfWork _efUnitOfWorkRepository;
        private readonly EfBookStoreContext _efBookStoreContext;

        public CreateOrderTests()
        {
            _connection = new SqliteConnection("datasource=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<EfBookStoreContext>()
                .UseSqlite(_connection)
            .Options;
            _efBookStoreContext = new EfBookStoreContext(options);
            _efUnitOfWorkRepository = new EfUnitOfWork(_efBookStoreContext);
        }

        [Fact]
        public async Task CreateOrderAsync_WhenOrderIsValid_ShouldReturnCorrectOrder()
        {
            // Arrange
            var testOrder = GetTestOrder();

            // Act
            var result = await _efUnitOfWorkRepository.Orders.CreateAsync(testOrder, 
                CancellationToken.None);
            await _efUnitOfWorkRepository.SaveChangesAsync(CancellationToken.None);
            testOrder = _efBookStoreContext.Orders!.FirstOrDefault(o=>o.Id==testOrder.Id);

            // Assert
            Assert.IsType<Order>(result);
            Assert.True(testOrder!.Id == result.Id && testOrder!.Email == result.Email &&
                testOrder.Name == result.Name && testOrder.Address == result.Address && 
                testOrder.OrderDate == result.OrderDate && testOrder.Total == result.Total &&
                testOrder.CartItems!.Count ==result.CartItems!.Count);
            Assert.True(testOrder!.CartItems?[0].Id == result.CartItems![0].Id &&
                testOrder.CartItems[0].OrderId == result.CartItems[0].OrderId &&
                testOrder.CartItems[0].ProductId == result.CartItems[0].ProductId &&
                testOrder.CartItems[0].Count == result.CartItems[0].Count);
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

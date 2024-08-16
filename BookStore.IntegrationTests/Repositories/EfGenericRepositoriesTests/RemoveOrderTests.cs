using Domain.EfRepositoriesInterfaces;
using Domain.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Persistence.EfData;
using Xunit;

namespace BookStore.FunctionalTests.Repositories.EfGenericRepositoriesTests
{
    public class RemoveOrderTests : IDisposable
    {
        private readonly SqliteConnection _connection;
        private readonly IEfUnitOfWork _efUnitOfWorkRepository;
        private readonly EfBookStoreContext _efBookStoreContext;
        public RemoveOrderTests()
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
        public async Task DeleteOrder_ExistingOrderId_ShouldReturnCorrectNoContent()
        {
            // Arrange
            var testOrder = GetTestOrder();

            // Act
            await _efUnitOfWorkRepository.Orders.CreateAsync(testOrder,
                CancellationToken.None);
            _efBookStoreContext.SaveChanges();

            List<Order> testOrderList =await _efBookStoreContext.Orders!.ToListAsync();

            _efUnitOfWorkRepository.Orders.Remove(testOrder.Id, CancellationToken.None);
            await _efUnitOfWorkRepository.SaveChangesAsync(CancellationToken.None);

            var result = _efBookStoreContext.Orders?.ToList();

            // Assert
            Assert.IsType<List<Order>>(result);
            Assert.True(result?.Count == 0);
            Assert.False(testOrderList.Count == result?.Count);
        }

        private Order GetTestOrder()
        {
            var testOrder = new Order
            {
                Id = 1,
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

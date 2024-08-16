using Domain.EfRepositoriesInterfaces;
using Domain.Entities;
using Moq;
using WebApi.CqrsMediatrFeatures.CqrsOrders.Commands.DeleteOrder;
using Xunit;

namespace BookStoreUnitTests.Orders.Commands
{
    public class DeleteOrderCommandHandlerTests
    {
        private readonly Mock<IEfUnitOfWork> _mockEfUnitOfWork;
        
        public DeleteOrderCommandHandlerTests()
        {
            _mockEfUnitOfWork = new();           
        }

        [Fact]
        public async Task DeleteOrderCommandHandlerAsync_ShouldReturnCorrectOrderId()
        {     
            // Arrange
            var testOrder=GetTestOrder();
            _mockEfUnitOfWork.Setup(r => r.Orders.Remove(testOrder.Id, CancellationToken.None))
                .Returns(testOrder.Id);          

            var handler = new DeleteOrderCommandHandler(_mockEfUnitOfWork.Object);

            // Act
            var result = await handler.Handle(
                new DeleteOrderCommand
                {
                    OrderId = testOrder.Id,
                },
            CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<DeleteOrderDto>(result);
            Assert.Equal(testOrder.Id, result!.OrderId);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(999)]
        public async Task DeleteOrderCommandHandlerAsync_OrderIdIsZero_Or_OutOfRange_ShouldReturnNull
            (int value)
        {
            // Arrange
            var testOrder = GetTestOrder();
            _mockEfUnitOfWork.Setup(r => r.Orders.Remove(testOrder.Id, CancellationToken.None))
                .Returns(0);

            var handler = new DeleteOrderCommandHandler(_mockEfUnitOfWork.Object);

            // Act
            var result = await handler.Handle(
                new DeleteOrderCommand
                {
                    OrderId = value,
                },
            CancellationToken.None);

            // Assert
            Assert.True(result == null);
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
                      new CartItem() {Id = 1, OrderId = 1, ProductId = 2, Count = 2}
                     }
            };
            return testOrder;
        }
    }  
}
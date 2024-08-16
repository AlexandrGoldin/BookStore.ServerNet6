using Application.ApplicationDTOs;
using Application.ServicesInterfaces;
using Moq;
using WebApi.CqrsMediatrFeatures.CqrsOrders.Queries.GetOrderList;
using Xunit;

namespace BookStoreUnitTests.Orders.Queries
{
    public class GetOrderListQueryHandlerTests
    {
        private readonly Mock<IOrderService> _mockOrderService;

        public GetOrderListQueryHandlerTests()
        {
            _mockOrderService = new();           
        }

        [Fact]
        public async Task GetOrderListQueryHandlerAsync_ShouldReturnAllOrders()
        {
            // Arrange
            var testOrders = GetTestOrders();
            _mockOrderService.Setup(s => s.GetListAsync(CancellationToken.None))
               .ReturnsAsync(testOrders);

            var handler = new GetOrderListQueryHandler(_mockOrderService.Object);

            // Act
            var result = await handler.Handle(new GetOrderListQuery(),
                 CancellationToken.None);

            // Assert
            Assert.IsType<List<OrderReadDto>>(result);
            Assert.Equal(testOrders.Count, result.Count);           
        }

        private List<OrderReadDto> GetTestOrders()
        {
            var testOrders = new List<OrderReadDto>();
            testOrders.Add(
                  new OrderReadDto
                  {
                      Id = 1, Email = "Email.Test_1@mail.com", Name = "TestName_1",
                      Address = "TestAddress 1", OrderDate = DateTime.Now, Total = 100.00m,
                      CartItems = new List<CartItemReadDto>()
                     {
                      new CartItemReadDto() {Id = 1, OrderId = 1, ProductId = 2, Count = 2}
                     }            
                  });
            testOrders.Add(
                 new OrderReadDto
                 {
                     Id = 2, Email = "Email.Test_2@mail.com", Name = "TestName_2",
                     Address = "TestAddress 2", OrderDate = DateTime.Now, Total = 200.00m,
                     CartItems = new List<CartItemReadDto>()
                     {
                      new CartItemReadDto() {Id = 2, OrderId = 2, ProductId = 1, Count = 3}
                     }
                 });             
            return testOrders;
        }
    }
}

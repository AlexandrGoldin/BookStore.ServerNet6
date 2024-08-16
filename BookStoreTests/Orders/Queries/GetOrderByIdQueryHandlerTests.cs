using Application.ApplicationDTOs;
using Application.ApplicationMappingProfiles;
using AutoMapper;
using Domain.DapperRepositoriesInterfaces;
using Domain.Entities;
using Moq;
using WebApi.CqrsMediatrFeatures.CqrsOrders.Queries.GetOrderById;
using Xunit;

namespace BookStoreUnitTests.Orders.Queries
{
    public class GetOrderByIdQueryHandlerTests
    {
        private readonly Mock<IDapperOrderRepository> _mockDapperOrderRepo;
        private readonly Mock<IDapperCartItemRepository> _mockCapperCartItemRepo;
        private readonly IMapper _mapper;

        public GetOrderByIdQueryHandlerTests()
        {
            _mockDapperOrderRepo = new ();
            _mockCapperCartItemRepo = new();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<CartItemProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetOrderByIdQueryHandlerAsync_ShouldReturnCorrectOrder()
        {
            //Arrange
            var testOrder = GetTestOrder();
            _mockDapperOrderRepo.Setup(o => o.GetByIdAsync(testOrder.Id, CancellationToken.None))
               .ReturnsAsync(testOrder);
            _mockCapperCartItemRepo.Setup(ci => ci.GetCartItemListByOrderIdAsync (testOrder.Id, CancellationToken.None))
               .ReturnsAsync(testOrder.CartItems);

            var handler = new GetOrderByIdQueryHandler(_mockDapperOrderRepo.Object, _mapper,
                _mockCapperCartItemRepo.Object);

            //Act
            var result = await handler.Handle(
                new GetOrderByIdQuery
                {
                    OrderId = testOrder.Id
                },
                 CancellationToken.None);

            //Assert
            Assert.IsType<OrderReadDto>(result);
            Assert.True(testOrder.Email == result!.Email && testOrder.Name == result.Name && 
                testOrder.Address == result.Address && testOrder.OrderDate == result.OrderDate &&
                testOrder.Total == result.Total);
            Assert.True(testOrder.CartItems!.Count == result.CartItems!.Count);
            Assert.True(testOrder.CartItems[0].ProductId == result.CartItems[0].ProductId &&
                testOrder.CartItems[0].OrderId == result.CartItems[0].OrderId &&
                testOrder.CartItems[0].Count == result.CartItems[0].Count &&
                testOrder.CartItems[0].Id == result.CartItems[0].Id);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(999)]
        public async Task GetOrderByIdQueryHandlerAsync_OrderIdIsOutOfRange_Or_Zero_ShouldReturnNull
            (int value)
        {
            //Arrange
            var testOrder = GetTestOrder();
            _mockDapperOrderRepo.Setup(o => o.GetByIdAsync(testOrder.Id, CancellationToken.None))
               .ReturnsAsync(testOrder);
            _mockCapperCartItemRepo.Setup(ci => ci.GetCartItemListByOrderIdAsync(testOrder.Id, CancellationToken.None))
               .ReturnsAsync(testOrder.CartItems);

            var handler = new GetOrderByIdQueryHandler(_mockDapperOrderRepo.Object, _mapper,
                _mockCapperCartItemRepo.Object);

            //Act

            var result = await handler.Handle(
                new GetOrderByIdQuery
                {
                    OrderId = value
                },
                 CancellationToken.None);

            //Assert

            Assert.Null(result);
            Assert.True(result == null);
        }

        private Order GetTestOrder()
        {
            var testOrder = new Order
            {
                Id = 1,
                Email = "Email.Test_1@mail.com",
                Name = "TestName_1",
                Address = "TestAddress_1",
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

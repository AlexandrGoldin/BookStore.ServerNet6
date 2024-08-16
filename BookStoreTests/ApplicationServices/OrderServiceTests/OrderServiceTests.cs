using Application.ApplicationDTOs;
using Application.ApplicationMappingProfiles;
using Application.ServicesImplementation;
using Application.ServicesInterfaces;
using AutoMapper;
using Domain.DapperRepositoriesInterfaces;
using Domain.Entities;
using Moq;
using Xunit;

namespace BookStoreUnitTests.ApplicationServices.OrderServiceTests
{
    public class OrderServiceTests
    {
        private readonly Mock<IDapperOrderRepository> _mockDapperOrderRepo;
        private readonly Mock<ICartItemService> _mockCartItemService;
        private readonly IMapper _mapper;

        public OrderServiceTests()
        {
            _mockDapperOrderRepo = new();
            _mockCartItemService = new();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<CartItemProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetOrderGetByIdAsync_ShouldReturnCorrectOrder()
        {
            // Arrange
            var testOrder = GetTestOrder();
            _mockDapperOrderRepo.Setup(r => r.GetByIdAsync(1, CancellationToken.None))
               .ReturnsAsync(testOrder);
            _mockCartItemService.Setup(s => s.GetCartItemListByOrderIdAsync(1,
                CancellationToken.None)).ReturnsAsync(testOrder.CartItems);

            var orderService = new OrderService(_mockDapperOrderRepo.Object, _mapper,
                _mockCartItemService.Object);

            // Act
            var result = await orderService.GetByIdAsync(1, CancellationToken.None);

            // Assert
            Assert.IsType<OrderReadDto>(result);
            Assert.True(testOrder.Email== result!.Email && testOrder.Name == result.Name &&
                testOrder.Address == result.Address && testOrder.OrderDate == result.OrderDate &&
                testOrder.Total == result.Total);
            Assert.True(testOrder.CartItems!.Count() == result.CartItems!.Count());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(999)]
        public async Task GetOrderGetByIdAsync_OrderIdIsZero_Or_OutOfRange_ShouldReturnNull
            (int value)
        {
            // Arrange
            var testOrder = GetTestOrder();
            _mockDapperOrderRepo.Setup(r => r.GetByIdAsync(1, CancellationToken.None))
               .ReturnsAsync(testOrder);
            _mockCartItemService.Setup(s => s.GetCartItemListByOrderIdAsync(1,
                CancellationToken.None)).ReturnsAsync(testOrder.CartItems);

            var orderService = new OrderService(_mockDapperOrderRepo.Object, _mapper,
                _mockCartItemService.Object);

            // Act
            var result = await orderService.GetByIdAsync(value, CancellationToken.None);

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

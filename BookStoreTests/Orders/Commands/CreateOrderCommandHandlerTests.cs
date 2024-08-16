using Application.ApplicationDTOs;
using Application.ApplicationMappingProfiles;
using Application.ServicesInterfaces;
using AutoMapper;
using Domain.EfRepositoriesInterfaces;
using Domain.Entities;
using Moq;
using WebApi.CqrsMediatrFeatures.CqrsOrders.Commands.CreateOrder;
using Xunit;

namespace BookStoreUnitTests.Orders.Commands
{
    public class CreateOrderCommandHandlerTests
    {
        private readonly Mock<IEfUnitOfWork> _mockEfUnitOfWork;
        private readonly Mock<ICartItemService> _mockCartItemService;
        private readonly IMapper _mapper;

        public CreateOrderCommandHandlerTests()
        {
            _mockEfUnitOfWork = new();  
            _mockCartItemService = new();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<CartItemProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
    }

        [Fact]
        public async Task CreateOrderCommandHandlerAsync_ShouldReturnCorrectOrder()
        {
            // Arrange
            var testOrder=GetTestOrder();
           
            _mockEfUnitOfWork.Setup(r => r.Orders.CreateAsync(testOrder, CancellationToken.None))
                .ReturnsAsync(testOrder);
            _mockCartItemService.Setup(s => s.GetCartItemListByOrderIdAsync(testOrder.Id, 
                CancellationToken.None)).ReturnsAsync(testOrder.CartItems!);

            var handler = new CreateOrderCommandHandler(_mockEfUnitOfWork.Object,
               _mapper, _mockCartItemService.Object);

            // Act
            var result = await handler.Handle(
                new CreateOrderCommand
                {
                    Email = testOrder.Email,
                    Name = testOrder.Name,
                    Address = testOrder.Address,
                    OrderDate = testOrder.OrderDate,
                    Total = testOrder.Total,
                    CartItems = testOrder.CartItems
                },
                CancellationToken.None);

            var testOrderDateString = testOrder.OrderDate.ToString("dd.MM.yyyy");
            var resultOrderDateString = result.OrderDate.ToString("dd.MM.yyyy");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OrderReadDto>(result);
            Assert.True(testOrder.Email == result.Email && testOrder.Name == result.Name &&
                testOrder.Address == result.Address && testOrderDateString == resultOrderDateString &&
                testOrder.Total == result.Total);
            Assert.True(testOrder.CartItems!.Count() == result.CartItems!.Count());
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
                      new CartItem() {ProductId = 2, Count = 2}
                     }
            };
            return testOrder;
        }      
    }
}

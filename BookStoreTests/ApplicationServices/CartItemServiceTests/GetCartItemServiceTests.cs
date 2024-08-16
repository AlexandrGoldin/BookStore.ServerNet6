using Application.ServicesImplementation;
using Domain.DapperRepositoriesInterfaces;
using Domain.Entities;
using Moq;
using Xunit;

namespace BookStoreUnitTests.ApplicationServices.CartItemServiceTests
{
    public class GetCartItemServiceTests
    {
        private readonly Mock<IDapperCartItemRepository> _mockDapperCartItemRepo;

        public GetCartItemServiceTests()
        {
            _mockDapperCartItemRepo = new();          
        }

        [Fact]
        public async Task GetCartItemListByOrderIdAsync_ShouldReturnCorrectListCartItem()
        {
            // Arrange
            var testCartItems = GetTestCartItems();
            _mockDapperCartItemRepo.Setup(r => r.GetCartItemListByOrderIdAsync(2,
                CancellationToken.None)).ReturnsAsync(testCartItems);

            // Act
            var cartItemService = new CartItemService(_mockDapperCartItemRepo.Object);

            var result=await cartItemService.GetCartItemListByOrderIdAsync(2, CancellationToken.None);

            // Assert
            Assert.IsType<List<CartItem>>(result);
            Assert.Equal(testCartItems, result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(999)]
        public async Task GetCartItemListByOrderIdAsync_OrderIdIsZero_Or_OutOfRange_ShouldReturnNull
            (int value)
        {
            // Arrange
            var testCartItems = GetTestCartItems();
            _mockDapperCartItemRepo.Setup(r => r.GetCartItemListByOrderIdAsync(2,
                CancellationToken.None)).ReturnsAsync(testCartItems);

            // Act
            var cartItemService = new CartItemService(_mockDapperCartItemRepo.Object);

            var result = await cartItemService.GetCartItemListByOrderIdAsync
                (value, CancellationToken.None);

            // Assert
            Assert.True(result == null);
        }

        private List<CartItem>  GetTestCartItems()
        {
            var testCartItems = new List<CartItem>();
            testCartItems.Add(new CartItem { Id = 2, OrderId = 2, ProductId = 1, Count = 2 });
            testCartItems.Add(new CartItem { Id = 3, OrderId = 2, ProductId = 3, Count = 1 });

            return testCartItems;
        }
    }
}

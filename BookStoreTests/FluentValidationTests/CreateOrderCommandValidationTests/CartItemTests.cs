using Domain.Entities;
using FluentValidation.TestHelper;
using WebApi.CqrsMediatrFeatures.CqrsOrders.Commands.CreateOrder;
using Xunit;

namespace BookStoreUnitTests.FluentValidationTests.CreateOrderCommandValidationTests
{
    public class CartItemTests
    {
        private readonly CreateOrderCommandValidator.CartItemValidator _cartItemValidator;

        public CartItemTests()
        {
            _cartItemValidator = new CreateOrderCommandValidator.CartItemValidator();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void CartItem_Count_should_be_GreaterThenZero(int count)
        {
            // Arrange
            var testCartItems = GetTestCartItem();
            testCartItems[0].Count = count;

            //Act
            var result = _cartItemValidator.TestValidate(testCartItems[0]);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Count);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void CartItem_ProductId_should_be_GreaterThenZero(int productId)
        {
            // Arrange
            var testCartItems = GetTestCartItem();
            testCartItems[0].ProductId = productId;

            //Act
            var result = _cartItemValidator.TestValidate(testCartItems[0]);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.ProductId);
        }

        private List<CartItem> GetTestCartItem()
        {
            var testCartItems = new List<CartItem>()
                     {
                      new CartItem() {ProductId = 2, Count = 2}
                     };
            return testCartItems;
        }
    }
}

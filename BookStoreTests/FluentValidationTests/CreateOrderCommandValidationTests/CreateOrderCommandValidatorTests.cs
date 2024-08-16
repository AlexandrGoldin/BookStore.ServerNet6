using Domain.Entities;
using FluentValidation.TestHelper;
using WebApi.CqrsMediatrFeatures.CqrsOrders.Commands.CreateOrder;
using Xunit;

namespace BookStoreUnitTests.FluentValidationTests.CreateOrderCommandValidationTests
{
    public class CreateOrderCommandTests
    {
        private readonly CreateOrderCommandValidator _createOrderValidator;

        public CreateOrderCommandTests()
        {
            _createOrderValidator = new CreateOrderCommandValidator();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Name_cannot_be_Empty(string name)
        {
            // Arrange
            var testOrder = GetTestOrder();
            testOrder.Name = name;

            //Act
            var result = _createOrderValidator.TestValidate(testOrder);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("EmailAddress")]
        [InlineData("Email@")]
        [InlineData("@Email")]
        public void Email_cannot_be_InCorrect(string email)
        {
            // Arrange
            var testOrder = GetTestOrder();
            testOrder.Email = email;

            //Act
            var result = _createOrderValidator.TestValidate(testOrder);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("123456789")]
        [InlineData("aaaaaaaaa")]
        public void Address_cannot_be_LessThanCharacters(string address)
        {
            // Arrange
            var testOrder = GetTestOrder();
            testOrder.Address = address;

            //Act
            var result = _createOrderValidator.TestValidate(testOrder);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Address);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Total_should_be_GreaterThenZero(decimal total)
        {
            // Arrange
            var testOrder = GetTestOrder();
            testOrder.Total = total;

            //Act
            var result = _createOrderValidator.TestValidate(testOrder);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Total);
        }

        private CreateOrderCommand GetTestOrder()
        {
            var testOrder = new CreateOrderCommand
            {
                Email = "Email.Test_1@mail.com",
                Name = "TestName_!",
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

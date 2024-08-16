using FluentValidation.TestHelper;
using WebApi.CqrsMediatrFeatures.CqrsOrders.Commands.DeleteOrder;
using Xunit;

namespace BookStoreUnitTests.FluentValidationTests.DeleteOrderCommandValidatorTests
{
    public class DeleteOrderCommandTests
    {
        private readonly DeleteOrderCommandValidator _deleteOrderCommandValidator;

        public DeleteOrderCommandTests()
        {
            _deleteOrderCommandValidator = new DeleteOrderCommandValidator();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void DeleteOrderCommand_OrderId_should_be_GreaterThenZero(int orderId)
        {
            // Arrange
            var testOrder = GetTestOrder();
            testOrder.OrderId = orderId;

            //Act
            var result = _deleteOrderCommandValidator.TestValidate(testOrder);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.OrderId);
        }

        public DeleteOrderCommand GetTestOrder()
        {
            var testOrder = new DeleteOrderCommand
            {
                OrderId = 1
            };
            return testOrder;
        }
    }
}

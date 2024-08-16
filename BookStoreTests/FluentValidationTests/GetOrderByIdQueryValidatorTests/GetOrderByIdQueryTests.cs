using FluentValidation.TestHelper;
using WebApi.CqrsMediatrFeatures.CqrsOrders.Queries.GetOrderById;
using Xunit;

namespace BookStoreUnitTests.FluentValidationTests.GetOrderByIdQueryValidatorTests
{
    public class GetOrderByIdQueryTests
    {
        private readonly GetOrderByIdQueryValidator _getOrderByIdQueryValidator;

        public GetOrderByIdQueryTests()
        {
            _getOrderByIdQueryValidator = new GetOrderByIdQueryValidator();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void GetOrderByIdQuery_OrderId_should_be_GreaterThenZero(int orderId)
        {
            // Arrange
            var testOrder = GetTestOrder();
            testOrder.OrderId = orderId;

            //Act
            var result = _getOrderByIdQueryValidator.TestValidate(testOrder);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.OrderId);
        }

        public GetOrderByIdQuery GetTestOrder()
        {
            var testOrder = new GetOrderByIdQuery
            {
                OrderId = 1
            };
            return testOrder;
        }
    }
}

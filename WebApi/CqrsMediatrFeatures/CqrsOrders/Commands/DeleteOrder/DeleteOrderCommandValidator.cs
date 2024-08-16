using FluentValidation;

namespace WebApi.CqrsMediatrFeatures.CqrsOrders.Commands.DeleteOrder
{
    public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrderCommandValidator() 
        {
            RuleFor(x => x.OrderId).GreaterThan(0)
                .WithMessage("OrderId is required.");
        }
    }
}

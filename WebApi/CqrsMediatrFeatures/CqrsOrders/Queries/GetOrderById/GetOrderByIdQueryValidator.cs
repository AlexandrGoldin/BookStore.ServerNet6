using FluentValidation;

namespace WebApi.CqrsMediatrFeatures.CqrsOrders.Queries.GetOrderById
{
    public class GetOrderByIdQueryValidator : AbstractValidator<GetOrderByIdQuery>
    {
        public GetOrderByIdQueryValidator() 
        {
            RuleFor(x => x.OrderId).GreaterThan(0)
                .WithMessage("OrderId is required end should be greater than zero");
        }
    }
}

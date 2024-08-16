using FluentValidation;

namespace WebApi.CqrsMediatrFeatures.CqrsProducts.Queries.GetProductById
{
    public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
    {
        public GetProductByIdQueryValidator()
        {
            RuleFor(x => x.ProductId).NotNull();
        }
    }
}

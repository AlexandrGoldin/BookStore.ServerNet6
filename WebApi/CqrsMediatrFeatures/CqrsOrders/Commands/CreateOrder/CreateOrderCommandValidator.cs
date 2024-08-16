using Domain.Entities;
using FluentValidation;

namespace WebApi.CqrsMediatrFeatures.CqrsOrders.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.Email)
               .EmailAddress().NotEmpty().WithMessage("The Email is incorrect");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(25).WithMessage("Name is required.");
            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MinimumLength(10).WithMessage("Address minimum length from 10 char");
            RuleFor(x => x.Total)
                .GreaterThan(0).WithMessage("Total is required.");
            RuleForEach(x => x.CartItems).SetValidator(new CartItemValidator());         
        }

        public class CartItemValidator : AbstractValidator<CartItem>
        {
            public CartItemValidator()
            {
                RuleFor(x => x.ProductId).GreaterThan(0)
                   .WithMessage("ProductId cannot be null or empty..");
                RuleFor(x => x.Count).GreaterThan(0)
                   .WithMessage("Count cannot be null or empty..");
            }
        }
    }
}

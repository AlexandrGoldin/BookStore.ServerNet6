using Application.ApplicationDTOs;
using Domain.Entities;
using MediatR;

namespace WebApi.CqrsMediatrFeatures.CqrsOrders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<OrderReadDto>
    {
        public string? Email { get; set; }

        public string? Name { get; set; }

        public string? Address { get; set; }

        public DateTime? OrderDate { get; set; } = default(DateTime?);
        public decimal Total { get; set; }

        public List<CartItem>? CartItems { get; set; } = new();
    }
}

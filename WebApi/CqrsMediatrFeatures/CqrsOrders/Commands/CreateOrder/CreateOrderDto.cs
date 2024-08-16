namespace WebApi.CqrsMediatrFeatures.CqrsOrders.Commands.CreateOrder
{
    public class CreateOrderDto
    {
        public string? Email { get; set; }

        public string? Name { get; set; }

        public string? Address { get; set; }

        public DateTime? OrderDate { get; set; } = default(DateTime?);

        public decimal Total { get; set; }

        public List<CreateCartItemDto>? CartItems { get; set; } = new();
    }
}

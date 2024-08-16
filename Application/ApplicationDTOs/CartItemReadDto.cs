namespace Application.ApplicationDTOs
{
    public class CartItemReadDto
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Count { get; set; }

        public ProductForGetOrderDto? Product { get; set; }

        public OrderReadDto? Order { get; set; }
    }
}

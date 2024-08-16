namespace Application.ApplicationDTOs
{
    public class OrderReadDto
    {
        public int Id { get; set; }

        public string? Email { get; set; }

        public string? Name { get; set; }

        public string? Address { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal Total { get; set; }

        public List<CartItemReadDto>? CartItems { get; set; } = new();
    }
}

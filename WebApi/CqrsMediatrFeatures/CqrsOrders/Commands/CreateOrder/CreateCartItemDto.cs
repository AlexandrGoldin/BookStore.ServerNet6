using Application.ApplicationDTOs;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApi.CqrsMediatrFeatures.CqrsOrders.Commands.CreateOrder
{
    public class CreateCartItemDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonPropertyName("cartItemId")]
        public int Id { get; set; }

        [JsonIgnore]
        public int OrderId { get; set; }

        [JsonPropertyName("id")]
        public int ProductId { get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonIgnore]
        public CreateProductDto? Product { get; set; }

        [JsonIgnore]
        public CreateOrderDto? Order { get; set; }
    }
}


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class CartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonPropertyName("cartItemId")]
        public int Id { get; set; }

        [Required]
        [JsonIgnore]
        public int OrderId { get; set; }

        [Required]
        [JsonPropertyName("id")]
        public int ProductId { get; set; }

        [Required]
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonIgnore]
        public Product? Product { get; set; } 

        [JsonIgnore]
        public Order? Order { get; set; }
    }
}

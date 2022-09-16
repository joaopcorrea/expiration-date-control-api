using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ExpirationDateControl_API.Models
{
    public class Product
    {
        [Key]
        [JsonPropertyName("id")]
        public int? Id { get; set; }
        [JsonPropertyName("barcode")]
        public string? Barcode { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("create_date")]
        public DateTime? CreateDate { get; set; }
        [JsonPropertyName("price")]
        public double? Price { get; set; }
        [JsonPropertyName("quantity")]
        public int? Quantity { get; set; }

        public Product Clone()
        {
            return (Product)this.MemberwiseClone(); // Shallow Clone
        }
    }
}

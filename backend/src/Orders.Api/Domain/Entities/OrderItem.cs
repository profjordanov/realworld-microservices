using System.ComponentModel.DataAnnotations;

namespace Orders.Api.Domain.Entities
{
    public class OrderItem
    {
        [Required]
        public uint ProductId { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
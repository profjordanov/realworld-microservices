using System;
using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.Api.Domain.Entities
{
    public class Product
    {
        [Required]
        public uint Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Base64Image { get; set; }

        public bool IsAvailable => Quantity > 0;

        public uint Quantity { get; set; }

        public uint DecreaseQuantity(uint value)
        {
            if (value > Quantity)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            Quantity -= value;
            return Quantity;
        }

        public ProductView ToView()
        {
            return new ProductView
            {
                Id = Id,
                Name = Name,
                Price = Convert.ToDouble(Price),
                IsAvailable = IsAvailable
            };
        }
    }
}
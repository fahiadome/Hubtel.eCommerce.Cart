using System;
using System.ComponentModel.DataAnnotations;

namespace Hubtel.eCommerce.Cart.Api.Infrastructure.Models
{
    public class AddCartModel
    {
        [Required]
        public Guid ItemId { get; set; }

        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be greater than zero (0)")]
        public int Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

    }
}

using System;
using Hubtel.eCommerce.Cart.Api.Infrastructure.Helpers;
using Sieve.Attributes;

namespace Hubtel.eCommerce.Cart.Api.Infrastructure.Models
{
    public class CartViewModel
    {
        public Guid CartId { get; set; }
        public Guid ItemId { get; set; }

        public string ItemName { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Amount => Quantity * UnitPrice;

        public string  PhoneNumber { get; set; }

        public DateTime?  DateAdded { get; set; }

    }
}


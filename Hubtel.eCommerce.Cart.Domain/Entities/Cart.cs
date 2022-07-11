using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hubtel.eCommerce.Cart.Domain.Entities;

namespace Hubtel.eCommerce.Cart.Domain
{
    public class Cart : EntityBase
    {
        public long ItemId { get; set; }
        public Item Item { get; set; }

        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Amount => Quantity * UnitPrice;
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hubtel.eCommerce.Cart.Domain.Entities
{
    public class ItemCategory:EntityBase
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public ICollection<Item> Items { get; set; }

    }
}

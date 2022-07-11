using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hubtel.eCommerce.Cart.Domain.Entities
{
    public class Item : EntityBase
    {
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public string UnitPrice { get; set; }
        public string SKU { get; set; }
        public string Description { get; set; }

        public Guid VendorId { get; set; }
        public ItemVendor Vendor { get; set; }

        public Guid ItemCategoryId { get; set; }
        public ItemCategory ItemCategory  { get; set; }
    }
}

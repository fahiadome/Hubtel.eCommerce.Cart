using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hubtel.eCommerce.Cart.Api.Core.Domain.Enums;

namespace Hubtel.eCommerce.Cart.Domain.Entities
{
    public class Item : EntityBase
    {
        [Required]
        public string ItemName { get; set; }
        [Required]
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,4)"), Required]
        public decimal UnitPrice { get; set; }
        [Required]
        public string SKU { get; set; }
        public string Description { get; set; }

        public ItemStatus Status { get; set; }
        public Guid VendorId { get; set; }
        public ItemVendor Vendor { get; set; }

        public Guid ItemCategoryId { get; set; }
        public ItemCategory ItemCategory  { get; set; }
    }

}

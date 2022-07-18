using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hubtel.eCommerce.Cart.Domain.Entities;

namespace Hubtel.eCommerce.Cart
{
    public class Cart : EntityBase
    {
        [Required]
        public Guid ItemId { get; set; }
        public Item Item { get; set; }

        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,4)"), Required]
        public decimal UnitPrice { get; set; }

        //public string ItemName { get; set; }

        public decimal Amount => Quantity * UnitPrice;

        

        public void  IncreaseQuantityBy(int quantity)
        {
            Quantity += quantity;
            
        }
        public void WithCustomerId(Guid customerId)
        {
            CustomerId = customerId;
        }
        public void WithId(Guid id)
        {
            Id = id;
        }



    }

}

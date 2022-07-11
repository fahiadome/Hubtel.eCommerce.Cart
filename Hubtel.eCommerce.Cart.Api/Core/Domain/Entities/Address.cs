using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hubtel.eCommerce.Cart.Domain.Entities
{
    public class Address : EntityBase
    {
        public string PrimaryPhoneNumber { get; set; }
        public string OtherPhoneNumber { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PrimaryEmail { get; set; }
        public string OtherEmail { get; set; }
        public string ZipCode { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }

        public Guid ItemVendorId { get; set; }
        public ItemVendor ItemVendor { get; set; }

        public Guid CustomerId { get; set; }
        public Customer Customer  { get; set; }
    }
}

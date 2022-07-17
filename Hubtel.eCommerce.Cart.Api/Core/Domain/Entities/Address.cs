using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hubtel.eCommerce.Cart.Api.Core.Domain.Enums;

namespace Hubtel.eCommerce.Cart.Domain.Entities
{
    public class Address : EntityBase
    {
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string ZipCode { get; set; }
        public AddressType AddressType { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }

        public Guid? ItemVendorId { get; set; }
        public ItemVendor ItemVendor { get; set; }

        public Guid? CustomerId { get; set; }
        public Customer Customer  { get; set; }
    }
}

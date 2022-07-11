using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hubtel.eCommerce.Cart.Domain.Entities
{
    public class Customer : EntityBase
    {
        public string FirstName { get; set; }
        public string OtherName { get; set; }
        public string LastName { get; set; }
        public ICollection<Address> Addresses { get; set; }

        public Guid userId { get; set; }
    }

}

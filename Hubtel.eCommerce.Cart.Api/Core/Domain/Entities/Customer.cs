using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hubtel.eCommerce.Cart.Domain.Entities
{
    public class Customer : EntityBase
    {
        [Required]
        public string FirstName { get; set; }
        public string OtherName { get; set; }
        [Required]
        public string LastName { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }

        public Guid UserId { get; set; }
    }

}

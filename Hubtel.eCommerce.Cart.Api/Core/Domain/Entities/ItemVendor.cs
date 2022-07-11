﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Hubtel.eCommerce.Cart.Domain.Entities
{
    public class ItemVendor:EntityBase
    {
        [Required]
        public string Name { get; set; }

        public string Code { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }

        public virtual ICollection<Item> Items { get; set; }

    }

}
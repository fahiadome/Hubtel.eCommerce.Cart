using System;
using Hubtel.eCommerce.Cart.Api.Infrastructure.Helpers;

namespace Hubtel.eCommerce.Cart.Api.Infrastructure.Models
{
    public class CartViewParameters : PaginationParameters
    {
        public string QueryString { get; set; }
        public string SortOrder { get; set; }
        public DateTime? DateAdded { get; set; }
        public  int? Quantity { get; set; }
    }
}



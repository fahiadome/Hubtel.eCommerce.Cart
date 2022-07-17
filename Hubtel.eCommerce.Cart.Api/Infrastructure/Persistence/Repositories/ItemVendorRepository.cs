using Hubtel.eCommerce.Cart.Api.Infrastructure.Persistence.Interfaces;
using Hubtel.eCommerce.Cart.Domain.Entities;

namespace Hubtel.eCommerce.Cart.Api.Infrastructure.Persistence.Repositories
{
    public class ItemVendorRepository : Repository<ItemVendor>, IItemVendorRepository
    {
        public ItemVendorRepository(ECommerceDbContext context) : base(context)
        {
        }
    }
}

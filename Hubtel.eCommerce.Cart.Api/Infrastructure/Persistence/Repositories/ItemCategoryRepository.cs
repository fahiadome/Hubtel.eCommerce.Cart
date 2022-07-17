using Hubtel.eCommerce.Cart.Api.Infrastructure.Persistence.Interfaces;
using Hubtel.eCommerce.Cart.Domain.Entities;

namespace Hubtel.eCommerce.Cart.Api.Infrastructure.Persistence.Repositories
{
    public class ItemCategoryRepository : Repository<ItemCategory>, IItemCategoryRepository
    {
        public ItemCategoryRepository(ECommerceDbContext context) : base(context)
        {
        }
    }
}

using Hubtel.eCommerce.Cart.Api.Infrastructure.Persistence.Interfaces;
using Hubtel.eCommerce.Cart.Domain.Entities;

namespace Hubtel.eCommerce.Cart.Api.Infrastructure.Persistence.Repositories
{
    public class ItemRepository: Repository<Item>, IItemRepository
    {
        public ItemRepository(ECommerceDbContext context) : base(context)
        {
        }
    }
}

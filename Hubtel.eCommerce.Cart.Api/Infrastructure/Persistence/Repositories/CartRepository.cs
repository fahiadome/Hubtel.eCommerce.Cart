using Hubtel.eCommerce.Cart.Api.Infrastructure.Persistence.Interfaces;
using Hubtel.eCommerce.Cart.Domain.Entities;

namespace Hubtel.eCommerce.Cart.Api.Infrastructure.Persistence.Repositories
{
    public class CartRepository : Repository<Item>, ICartRepository
    {
        public CartRepository(ECommerceDbContext context) : base(context)
        {
        }
    }
}

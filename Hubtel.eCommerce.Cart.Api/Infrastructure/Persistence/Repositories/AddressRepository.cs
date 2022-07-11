using Hubtel.eCommerce.Cart.Api.Infrastructure.Persistence.Interfaces;
using Hubtel.eCommerce.Cart.Domain.Entities;

namespace Hubtel.eCommerce.Cart.Api.Infrastructure.Persistence.Repositories
{
    public class AddressRepository : Repository<Item>, IAddressRepository
    {
        public AddressRepository(ECommerceDbContext context) : base(context)
        {
        }
    }
}

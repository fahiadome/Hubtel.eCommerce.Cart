using Hubtel.eCommerce.Cart.Api.Infrastructure.Persistence.Interfaces;
using Hubtel.eCommerce.Cart.Domain.Entities;

namespace Hubtel.eCommerce.Cart.Api.Infrastructure.Persistence.Repositories
{
    public class CustomerRepository : Repository<Item>, ICustomerRepository
    {
        public CustomerRepository(ECommerceDbContext context) : base(context)
        {
        }
    }
}

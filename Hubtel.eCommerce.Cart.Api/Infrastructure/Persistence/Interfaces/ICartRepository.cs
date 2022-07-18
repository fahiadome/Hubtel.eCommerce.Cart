using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hubtel.eCommerce.Cart.Api.Infrastructure.Models;
using Hubtel.eCommerce.Cart.Api.Infrastructure.Persistence.Repositories;

namespace Hubtel.eCommerce.Cart.Api.Infrastructure.Persistence.Interfaces
{
    public interface ICartRepository : IRepository<Cart>
    {
        Task<IReadOnlyCollection<CartViewModel>> GetCartByUserIdAsync(QueryTerm queryTerm);
        Task<Cart> GetAllUserCartByIdAsync(Guid userId, Guid itemId);

    }
}

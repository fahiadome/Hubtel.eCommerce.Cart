﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hubtel.eCommerce.Cart.Api.Infrastructure.Helpers;
using Hubtel.eCommerce.Cart.Api.Infrastructure.Models;
using Hubtel.eCommerce.Cart.Api.Infrastructure.Persistence.Repositories;
using Hubtel.eCommerce.Cart.Domain.Entities;

namespace Hubtel.eCommerce.Cart.Api.Infrastructure.Persistence.Interfaces
{
    public interface ICartRepository : IRepository<Cart>
    {
        Task<IReadOnlyCollection<CartViewModel>> GetCartByUserIdAsync(QueryTerm queryTerm);
        Task<Cart> GetCartByUserIdAndItemIdAsync(Guid userId, Guid itemId);

    }
}

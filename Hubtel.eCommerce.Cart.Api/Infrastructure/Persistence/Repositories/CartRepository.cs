using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hubtel.eCommerce.Cart.Api.Infrastructure.Helpers;
using Hubtel.eCommerce.Cart.Api.Infrastructure.Models;
using Hubtel.eCommerce.Cart.Api.Infrastructure.Persistence.Interfaces;
using Hubtel.eCommerce.Cart.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Nest;

namespace Hubtel.eCommerce.Cart.Api.Infrastructure.Persistence.Repositories
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        private readonly ILogger<Cart> _logger;
        private readonly ECommerceDbContext _context;
        private readonly IElasticClient _elasticClient;

        public CartRepository(ILogger<Cart> logger, ECommerceDbContext context, IElasticClient elasticClient) : base(context)
        {
            _logger = logger;
            _context = context;
            _elasticClient = elasticClient;
        }

        public async Task<List<CartViewModel>> GetCartByUserIdAsync(Guid userId)
        {
            var dataToReturn = new List<CartViewModel>();

            var data = (from c in _context.Carts
                    .Include(a=>a.Item)
                    .Include(a=>a.Customer)
                    .ThenInclude(a=>a.Addresses)
                where c.Customer.UserId == userId
                    select c);

            foreach (var cart in data)
            {
                var cartViewModel = new CartViewModel()
                {
                    CartId = cart.Id,
                    ItemId = cart.ItemId,
                    ItemName = cart.Item?.ItemName ?? "",
                    Quantity = cart.Quantity,
                    UnitPrice = cart.UnitPrice,
                    PhoneNumber = cart?.Customer?.Addresses?.First()?.PhoneNumber ?? "",
                    DateAdded = cart.AddedDate
                };

                dataToReturn.Add(cartViewModel);
            }


            //var response = await _elasticClient.GetAsync<Cart>(new DocumentPath<Cart>(
            //    new Id(userId)), x => x.Index("carts"));

            //return response?.Source;  
                
            //    )

            return await Task.FromResult(dataToReturn);

        }

        public async Task<Cart> GetCartByUserIdAndItemIdAsync(Guid userId, Guid itemId)
        {
            try
            {
                var data = _context.Carts
                        .FirstOrDefaultAsync(a => a.Customer.UserId == userId && a.ItemId == itemId);

                return await data;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    }
}

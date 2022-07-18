using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hubtel.eCommerce.Cart.Api.Infrastructure.Models;
using Hubtel.eCommerce.Cart.Api.Infrastructure.Persistence.Interfaces;
using Hubtel.eCommerce.Cart.Api.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hubtel.eCommerce.Cart.Api.Infrastructure.Persistence.Repositories
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        private readonly ILogger<Cart> _logger;
        private readonly ECommerceDbContext _context;
        private readonly ElasticSearchService _elasticClient;

        public CartRepository(ILogger<Cart> logger, ECommerceDbContext context, ElasticSearchService elasticClient) : base(context)
        {
            _logger = logger;
            _context = context;
            _elasticClient = elasticClient;
        }

        public async Task<IReadOnlyCollection<CartViewModel>> GetCartByUserIdAsync(QueryTerm queryTerm)
        {

          var cart = await _elasticClient.GetCartBySearchTerm(queryTerm.PhoneNumber, queryTerm.ItemName, queryTerm.Quantity,
                queryTerm.Time);

          return cart;

        }

        public async Task<Cart> GetAllUserCartByIdAsync(Guid userId, Guid itemId)
        {
            try
            {
                return await _context.Carts.FirstOrDefaultAsync(a => a.ItemId == itemId && a.Customer.UserId == userId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    }

    public class QueryTerm
    {
        public string ItemName { get; set; }
        public string PhoneNumber { get; set; }
        public int Quantity { get; set; }
        public DateTime Time { get; set; }

    }
}

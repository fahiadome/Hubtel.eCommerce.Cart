using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Hubtel.eCommerce.Cart.Api.Infrastructure.Helpers;
using Hubtel.eCommerce.Cart.Api.Infrastructure.Models;
using Hubtel.eCommerce.Cart.Api.Infrastructure.Persistence.Interfaces;
using Hubtel.eCommerce.Cart.Api.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;

namespace Hubtel.eCommerce.Cart.Api.Core.Processors
{
    [ProcessorBase]
    public class CartProcessor
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public CartProcessor(ICartRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        public async Task<List<CartViewModel>> GetAllCartByUserId(Guid userId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);

            var data = _mapper.Map<List<CartViewModel>>(cart);

            return data;

        }
        public async Task<List<CartViewModel>> GetAllCartByUserIdAndItemId(Guid itemId)
        {
            var user = GetUserInfoFromToken();

            var cart = await _cartRepository.GetCartByUserIdAndItemIdAsync(user.Id, itemId);

            var data = _mapper.Map<List<CartViewModel>>(cart);

            return data;
        }

        public async Task RemoveCart(Guid id, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetAsync(id);
            
            if (cart == null)
            {
                throw new Exception("Not Found");
            }

            await _cartRepository.DeleteAsync(cart, cancellationToken);
        }

        public async Task AddCart(CartCommand model, CancellationToken cancellationToken)
        {
            try
            {
                var user = GetUserInfoFromToken();

                var cart = BuildCartModelFromCommand(model);

                var cartItem = await _cartRepository.GetCartByUserIdAndItemIdAsync(user.Id, model.ItemId);

                var isNewCart = cartItem == null;


                if (isNewCart)
                {
                    cart.WithAddDateAndUser(DateTime.Now, user.Id);
                    cart.WithModifiedDateAndUser(DateTime.Now, user.Id);
                    cart.WithCustomerId(new Guid("9DE9254D-C3DC-4AF2-810C-E924682DC173"));
                    await _cartRepository.InsertAsync(cart, cancellationToken);
                }
                else
                {
                    cartItem.IncreaseQuantityBy(model.Quantity);
                    cartItem.WithModifiedDateAndUser(DateTime.Now, user.Id);
                    cartItem.WithCustomerId(new Guid("9DE9254D-C3DC-4AF2-810C-E924682DC173"));
                    await _cartRepository.UpdateAsync(cartItem, cancellationToken);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        private LoginUser GetUserInfoFromToken()
        {
            return new LoginUser
            {
                Id = new Guid("d67e5380-a832-4f3d-ad42-5cba564f7ad8"),
                UserName = "fahiadome@umat.edu.gh",
                PhoneNumber = "0546019493"
            };
        }

        private static Cart BuildCartModelFromCommand(CartCommand model)
        {
            return new Cart()
            {
                ItemId = model.ItemId,
                Quantity = model.Quantity,
                UnitPrice = model.UnitPrice,
                ItemName = model.ItemName
            };
        }
    }

    internal class LoginUser
    {
        public Guid Id { get; set; } 
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }

    }

    public  class CartCommand
    {
        public Guid ItemId { get; set; }
        public int Quantity { get; set; }
        public string ItemName { get; set; }
        public decimal UnitPrice { get; set; }
    }

}

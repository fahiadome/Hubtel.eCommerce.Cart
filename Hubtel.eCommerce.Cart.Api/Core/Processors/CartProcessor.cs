using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Hubtel.eCommerce.Cart.Api.Infrastructure.Models;
using Hubtel.eCommerce.Cart.Api.Infrastructure.Persistence.Interfaces;
using Hubtel.eCommerce.Cart.Api.Infrastructure.Persistence.Repositories;
using Hubtel.eCommerce.Cart.Api.Service;
using Hubtel.eCommerce.Cart.Domain.Entities;

namespace Hubtel.eCommerce.Cart.Api.Core.Processors
{
    [ProcessorBase]
    public class CartProcessor
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        private readonly ElasticSearchService _elasticClient;
        private readonly IItemRepository _itemRepository;

        public CartProcessor(ICartRepository cartRepository, IMapper mapper, ElasticSearchService elasticClient, IItemRepository itemRepository)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _elasticClient = elasticClient;
            _itemRepository = itemRepository;
        }

        public async Task<IReadOnlyCollection<CartViewModel>> GetAllCartByUserId(QueryTerm queryTerm)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(queryTerm);

            var cartItem = _mapper.Map<List<CartViewModel>>(cart);
            
            return cartItem;

        }
        public async Task<CartViewModel> GetAllUserCartById(Guid itemId)
        {
            var user = GetUserInfoFromToken();

            var cart = await _cartRepository.GetAllUserCartByIdAsync(user.Id, itemId);

            var data = _mapper.Map<CartViewModel>(cart);

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

            await _elasticClient.DeleteAsync("cart", id, cancellationToken);
        }

        public async Task AddCart(CartCommand model, CancellationToken cancellationToken)
        {
            try
            {
                if (model.Quantity <= 0 || model.UnitPrice <= 0)
                    throw new Exception($"{nameof(model.Quantity) } or {nameof(model.UnitPrice)} cannot be zero (0)");
                var cartId = Guid.NewGuid();
                
                var user = GetUserInfoFromToken();

                var cart = BuildCartModelFromCommand(model);
                var cartItem = await _cartRepository.GetAllUserCartByIdAsync(user.Id, model.ItemId);

                var item = await _itemRepository.GetAsync(model.ItemId);
                var esCart = BuildEsCartModelFromCommand(model, user.PhoneNumber, item.ItemName);
                
                var isNewCart =  cartItem == null;
      

                if (isNewCart)
                {
                    SupplyNewCartInfo(cart, user, cartId);
                    await _cartRepository.InsertAsync(cart, cancellationToken);

                    SupplyNewEsCartInfo(esCart, user, cartId);
                    await _elasticClient.AddAsync("cart", esCart);
                }
                else
                {
                    SupplyUpdateCartInfo(model, esCart, user);
                    await _cartRepository.UpdateAsync(cartItem, cancellationToken);

                    SupplyUpdateEsCartInfo(model, cartItem, user);
                    await _elasticClient.AddAsync("cart", esCart);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        private static void SupplyUpdateEsCartInfo(CartCommand model, Cart esCart, LoginUser user)
        {
            esCart.WithAddDateAndUser(user.Id);
            esCart.IncreaseQuantityBy(model.Quantity);
        }

        private static void SupplyNewEsCartInfo(EsCartCommand esCart, LoginUser user, Guid id)
        {
            esCart.WithAddDateAndUser(user.Id);
            esCart.WithModifiedDateAndUser(user.Id);
            esCart.WithCustomerId(user.CustomerId);
            esCart.WithId(id);
        }

        private static void SupplyUpdateCartInfo(CartCommand model, EsCartCommand cartItem, LoginUser user)
        {
            cartItem.IncreaseQuantityBy(model.Quantity);
            cartItem.WithModifiedDateAndUser(user.Id);
            cartItem.WithCustomerId(user.CustomerId);
        }

        private static void SupplyNewCartInfo(Cart cart, LoginUser user, Guid id)
        {
            cart.WithAddDateAndUser(user.Id);
            cart.WithModifiedDateAndUser(user.Id);
            cart.WithCustomerId(user.CustomerId);
            cart.WithId(id);
        }

        private LoginUser GetUserInfoFromToken()
        {
            return new LoginUser
            {
                Id = new Guid("d67e5380-a832-4f3d-ad42-5cba564f7ad8"),
                UserName = "fahiadome@umat.edu.gh",
                PhoneNumber = "0546019493",
                CustomerId = new Guid("9DE9254D-C3DC-4AF2-810C-E924682DC173")
            };
        }

        private static Cart BuildCartModelFromCommand(CartCommand model)
        {
            return new Cart()
            {
                ItemId = model.ItemId,
                Quantity = model.Quantity,
                UnitPrice = model.UnitPrice,

            };
        }
        private static EsCartCommand BuildEsCartModelFromCommand(CartCommand model, string phoneNumber, string itemName)
        {
            return new EsCartCommand()
            {
                ItemId = model.ItemId,
                Quantity = model.Quantity,
                UnitPrice = model.UnitPrice,
                PhoneNumber = phoneNumber,
                ItemName = itemName,
                
            };
        }
    }

    internal class LoginUser
    {
        public Guid Id { get; set; } 
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public Guid CustomerId { get; set; }
    }

    public class CartCommand
    {
        public Guid ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
    public class EsCartCommand
    {
        public Guid Id;
        public Guid ItemId { get; set; }
        public Guid CustomerId { get; set; }
        public int Quantity { get; set; }
        public string ItemName { get; set; }
        public decimal UnitPrice { get; set; }
        public string PhoneNumber { get; set; }

        public DateTime AddedDate { get; set; }
        public string AddedBy { get; set; }
        public DateTime DateModified { get; set; }
        public string ModifiedBy { get; set; }


        public void WithAddDateAndUser(Guid userId)
        {
            AddedDate = DateTime.Now;
            AddedBy = userId.ToString();
        }
        public void WithModifiedDateAndUser(Guid userId)
        {
            DateModified = DateTime.Now;
            ModifiedBy = userId.ToString();
        }

        public void IncreaseQuantityBy(int quantity)
        {
            Quantity += quantity;

        }
        public void WithPhoneNumber(string phoneNumber)
        {
            PhoneNumber = phoneNumber;

        }
        public void WithId(Guid id)
        {
            Id = id;

        }

        public void WithCustomerId(Guid customerId)
        {
            CustomerId = customerId;

        }


    }

}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hubtel.eCommerce.Cart.Api.Core.Application.Features.Cart.Commands;
using Hubtel.eCommerce.Cart.Api.Core.Application.Features.Cart.Queries;
using Hubtel.eCommerce.Cart.Api.Infrastructure.Models;
using Hubtel.eCommerce.Cart.Api.Infrastructure.Persistence.Interfaces;
using Hubtel.eCommerce.Cart.Api.Infrastructure.Persistence.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Hubtel.eCommerce.Cart.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;
        private readonly ICartRepository _cartRepository;
        private readonly IMediator _mediator;

        public CartController(ILogger<CartController> logger, ICartRepository cartRepository, IMediator mediator  )
        {
            _logger = logger;
            _cartRepository = cartRepository;
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Add([FromBody] AddCart.Command command)
        {
           await _mediator.Send(command);

            return NoContent();
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IReadOnlyCollection<CartViewModel>> GetAll([FromQuery] QueryTerm queryTerm)
        {

            return await _mediator.Send(new GetCartById.Query(queryTerm));

        }

        [HttpGet("itemId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<CartViewModel> GetById(Guid itemId)
        {

            return await _mediator.Send(new GetCartByItemId.Query(itemId));

        }


        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new RemoveCart.Command {Id = id});

            return NoContent();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Hubtel.eCommerce.Cart.Api.Core.Application.Features.Cart.Commands;
using Hubtel.eCommerce.Cart.Api.Core.Application.Features.Cart.Queries;
using Hubtel.eCommerce.Cart.Api.Infrastructure.Helpers;
using Hubtel.eCommerce.Cart.Api.Infrastructure.Models;
using Hubtel.eCommerce.Cart.Api.Infrastructure.Persistence.Interfaces;
using Hubtel.eCommerce.Cart.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Nest;

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
            _mediator.Send(command);

            return NoContent();
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<List<CartViewModel>> Get()
        {
             var userId = new Guid("d67e5380-a832-4f3d-ad42-5cba564f7ad8");

            return await _mediator.Send(new GetCartById.Query(userId));

            // var result = await _cartRepository.GetCartByUserIdAsync(userId);

            //return Ok(result);
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

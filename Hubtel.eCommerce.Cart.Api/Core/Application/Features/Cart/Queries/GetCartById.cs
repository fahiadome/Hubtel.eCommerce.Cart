using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Hubtel.eCommerce.Cart.Api.Core.Processors;
using Hubtel.eCommerce.Cart.Api.Infrastructure.Helpers;
using Hubtel.eCommerce.Cart.Api.Infrastructure.Models;
using MediatR;

namespace Hubtel.eCommerce.Cart.Api.Core.Application.Features.Cart.Queries
{


    public static class GetCartById
    {
        public class Query : IRequest<List<CartViewModel>>
        {
            public Query(Guid userId)
            {
                UserId = userId;
            }

            public Guid UserId { get; }
        }

        public class Handler : IRequestHandler<Query, List<CartViewModel>>
        {
            private readonly CartProcessor _cartProcessor;

            public Handler(CartProcessor cartProcessor)
            {
                _cartProcessor = cartProcessor;
            }

            public async Task<List<CartViewModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _cartProcessor.GetAllCartByUserId(request.UserId);
            }
        }

        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {
                RuleFor(value => value.UserId).NotNull();
            }
        }
    }
}

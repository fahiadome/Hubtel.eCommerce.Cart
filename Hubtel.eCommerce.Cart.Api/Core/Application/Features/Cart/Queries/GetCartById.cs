using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Hubtel.eCommerce.Cart.Api.Core.Processors;
using Hubtel.eCommerce.Cart.Api.Infrastructure.Models;
using Hubtel.eCommerce.Cart.Api.Infrastructure.Persistence.Repositories;
using MediatR;

namespace Hubtel.eCommerce.Cart.Api.Core.Application.Features.Cart.Queries
{


    public static class GetCartById
    {
        public class Query : IRequest<IReadOnlyCollection<CartViewModel>>
        {
            public Query(QueryTerm queryTerm)
            {
                QueryTerm = queryTerm;
            }

            public QueryTerm QueryTerm  { get; }
        }

        public class Handler : IRequestHandler<Query, IReadOnlyCollection<CartViewModel>>
        {
            private readonly CartProcessor _cartProcessor;

            public Handler(CartProcessor cartProcessor)
            {
                _cartProcessor = cartProcessor;
            }

            public async Task<IReadOnlyCollection<CartViewModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _cartProcessor.GetAllCartByUserId(request.QueryTerm);
            }
        }

    }

    public static class GetCartByItemId
    {
        public class Query : IRequest<CartViewModel>
        {
            public Query(Guid itemId)
            {
                ItemId = itemId;
            }

            public Guid ItemId { get; }
        }

        public class Handler : IRequestHandler<Query, CartViewModel>
        {
            private readonly CartProcessor _cartProcessor;

            public Handler(CartProcessor cartProcessor)
            {
                _cartProcessor = cartProcessor;
            }

            public async Task<CartViewModel> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _cartProcessor.GetAllCartByUserIdAndItemId(request.ItemId);
            }
        }

        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {
                RuleFor(value => value.ItemId).NotNull();
            }
        }
    }

}

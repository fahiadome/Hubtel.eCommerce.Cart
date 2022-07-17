using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Hubtel.eCommerce.Cart.Api.Core.Processors;
using MediatR;

namespace Hubtel.eCommerce.Cart.Api.Core.Application.Features.Cart.Commands
{
    public static class AddCart
    {
        public class Command : IRequest<string>
        {
            public CartCommand Payload { get; set; }

        }

        public class Handler : IRequestHandler<Command, string>
        {
            private readonly CartProcessor _cartProcessor;

            public Handler(CartProcessor courseProcessor)
            {
                _cartProcessor = courseProcessor;
            }

            public async Task<string> Handle(Command request, CancellationToken cancellationToken)
            {
                await _cartProcessor.AddCart(request.Payload, cancellationToken);

                return request.Payload.ItemId.ToString();
            }
        }
    }
}

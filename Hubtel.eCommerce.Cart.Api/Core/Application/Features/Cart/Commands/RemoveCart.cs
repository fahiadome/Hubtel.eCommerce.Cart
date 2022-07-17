using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Hubtel.eCommerce.Cart.Api.Core.Processors;
using MediatR;

namespace Hubtel.eCommerce.Cart.Api.Core.Application.Features.Cart.Commands
{
    public static class RemoveCart
    {
        public class Command : IRequest<Guid>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Guid>
        {
            private readonly CartProcessor _cartProcessor;

            public Handler(CartProcessor courseProcessor)
            {
                _cartProcessor = courseProcessor;
            }
            public async Task<Guid> Handle(Command request, CancellationToken cancellationToken)
            {
                await _cartProcessor.RemoveCart(request.Id, cancellationToken);

                return request.Id;
            }
        }
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Id).NotEmpty();
            }
        }
    }
}

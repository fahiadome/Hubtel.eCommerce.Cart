using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Hubtel.eCommerce.Cart.Api.Infrastructure.Persistence.Interfaces;
using Hubtel.eCommerce.Cart.Api.Infrastructure.Persistence.Repositories;

namespace Hubtel.eCommerce.Cart.Api.Core.Processors
{
    public class ProcessorBase : Attribute
    {
    }
}

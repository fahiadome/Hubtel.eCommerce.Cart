using AutoMapper;
using Hubtel.eCommerce.Cart.Api.Core.Processors;
using Hubtel.eCommerce.Cart.Api.Infrastructure.Models;

namespace Hubtel.eCommerce.Cart.Api.Infrastructure.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddCartModel, Cart>().ReverseMap();
            CreateMap<CartViewModel, Cart>().ReverseMap();
        }
    }
}

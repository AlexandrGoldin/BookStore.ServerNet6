using Application.ApplicationDTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.ApplicationMappingProfiles
{
    public class CartItemProfile : Profile
    {
        public CartItemProfile()
        {
            CreateMap<CartItem, CartItemReadDto>().ReverseMap();
        }
    }
}

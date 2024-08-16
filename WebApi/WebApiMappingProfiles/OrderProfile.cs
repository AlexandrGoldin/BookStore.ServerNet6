using AutoMapper;
using WebApi.CqrsMediatrFeatures.CqrsOrders.Commands.CreateOrder;

namespace WebApi.WebApiMappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<CreateOrderDto, CreateOrderCommand>().ReverseMap();           
        }
    }
}

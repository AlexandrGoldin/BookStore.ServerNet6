using Application.ApplicationDTOs;
using MediatR;

namespace WebApi.CqrsMediatrFeatures.CqrsOrders.Queries.GetOrderById
{
    public class GetOrderByIdQuery : IRequest<OrderReadDto?>
    {
        public int OrderId {get;set;}
    }
}

using Application.ApplicationDTOs;
using MediatR;

namespace WebApi.CqrsMediatrFeatures.CqrsOrders.Queries.GetOrderList
{
    public class GetOrderListQuery : IRequest<List<OrderReadDto>>
    {
    }
}

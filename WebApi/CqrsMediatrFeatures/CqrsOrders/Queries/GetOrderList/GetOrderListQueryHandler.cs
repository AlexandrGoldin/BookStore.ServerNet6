using Application.ApplicationDTOs;
using Application.ServicesInterfaces;
using MediatR;

namespace WebApi.CqrsMediatrFeatures.CqrsOrders.Queries.GetOrderList
{
    public class GetOrderListQueryHandler
        : IRequestHandler<GetOrderListQuery, List<OrderReadDto>>
    {
        private readonly IOrderService _orderService;

        public GetOrderListQueryHandler(IOrderService orderService)
        {
           _orderService = orderService;
        }

        public async Task<List<OrderReadDto>> Handle(GetOrderListQuery request,
            CancellationToken cancellationToken)
        {
            await Task.Delay(1000);

            return (await _orderService.GetListAsync(cancellationToken)).ToList();       
        }
    }
}

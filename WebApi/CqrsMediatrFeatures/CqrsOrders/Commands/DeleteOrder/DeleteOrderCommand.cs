using MediatR;

namespace WebApi.CqrsMediatrFeatures.CqrsOrders.Commands.DeleteOrder
{
    public class DeleteOrderCommand : IRequest<DeleteOrderDto?>
    {
        public int OrderId { get; set; }
    }
}

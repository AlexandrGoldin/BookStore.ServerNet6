using Domain.EfRepositoriesInterfaces;
using MediatR;

namespace WebApi.CqrsMediatrFeatures.CqrsOrders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, DeleteOrderDto?>
    {
        private readonly IEfUnitOfWork _unitOfWork;

        public DeleteOrderCommandHandler(IEfUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DeleteOrderDto?> Handle(DeleteOrderCommand request, 
            CancellationToken cancellationToken)
        {
            var result = _unitOfWork.Orders.Remove(request.OrderId, cancellationToken);

            if (result == 0)
            {
                return null;
            }
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new DeleteOrderDto
            {
                OrderId = result
            };         
        }
    }
}

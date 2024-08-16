using Application.ApplicationDTOs;
using AutoMapper;
using Domain.DapperRepositoriesInterfaces;
using MediatR;

namespace WebApi.CqrsMediatrFeatures.CqrsOrders.Queries.GetOrderById
{
    public class GetOrderByIdQueryHandler 
        : IRequestHandler<GetOrderByIdQuery, OrderReadDto?>
    {
        private readonly IDapperOrderRepository _dapperOrderRepo;
        private readonly IDapperCartItemRepository _dapperCartItemRepo;
        private readonly IMapper _mapper;

        public GetOrderByIdQueryHandler(IDapperOrderRepository dapperOrderRepo, IMapper mapper,
            IDapperCartItemRepository dapperCartItemRepo)
        {
            _dapperOrderRepo = dapperOrderRepo;
            _dapperCartItemRepo = dapperCartItemRepo;
            _mapper = mapper;
        }
                      
        public async Task<OrderReadDto?> Handle(GetOrderByIdQuery request, 
            CancellationToken cancellationToken)
        {
            var order = await _dapperOrderRepo.GetByIdAsync(request.OrderId,
                cancellationToken);

            if (order == null)
            {
                return null;
            }
            return new OrderReadDto
            {
                Id = order.Id,
                Email = order.Email,
                Name = order.Name,
                Address = order.Address,
                OrderDate = order.OrderDate,
                Total = order.Total,
                CartItems = _mapper.Map<List<CartItemReadDto>>( await _dapperCartItemRepo
                    .GetCartItemListByOrderIdAsync(order.Id, cancellationToken))
             };
        }
    }
}

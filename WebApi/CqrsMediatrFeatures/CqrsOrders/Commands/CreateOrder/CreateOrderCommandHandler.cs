using Application.ApplicationDTOs;
using Application.ServicesInterfaces;
using AutoMapper;
using Domain.EfRepositoriesInterfaces;
using Domain.Entities;
using MediatR;

namespace WebApi.CqrsMediatrFeatures.CqrsOrders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderReadDto>
    {
        private readonly ICartItemService _cartItemService;
        private readonly IEfUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateOrderCommandHandler(IEfUnitOfWork unitOfWork, IMapper mapper,
            ICartItemService cartItemService)          
        {
            _cartItemService = cartItemService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderReadDto> Handle(CreateOrderCommand request, 
            CancellationToken cancellationToken)
        {
            var order = new Order
            {
                Email = request.Email,
                Name = request.Name,
                Address = request.Address,
                OrderDate = DateTime.Now,
                Total = request.Total,
                CartItems = request.CartItems
            };
            await _unitOfWork.Orders.CreateAsync(order, cancellationToken);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var cartItemList = await _cartItemService.GetCartItemListByOrderIdAsync(order.Id,
                    cancellationToken);

            return new OrderReadDto
            {
                Id = order.Id,
                Email = order.Email,
                Name = order.Name,
                Address = order.Address,
                OrderDate = order.OrderDate,
                Total = order.Total,
                CartItems = _mapper.Map<List<CartItemReadDto>>(cartItemList)
            };              
        }
    }
}

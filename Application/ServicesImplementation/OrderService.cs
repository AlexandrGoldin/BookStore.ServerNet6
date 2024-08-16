using Application.ApplicationDTOs;
using Application.ServicesInterfaces;
using AutoMapper;
using Domain.DapperRepositoriesInterfaces;

namespace Application.ServicesImplementation
{
    public class OrderService : IOrderService
    {
        private readonly IDapperOrderRepository _dapperOrderRepo;
        private readonly ICartItemService _cartItemService;
        private readonly IMapper _mapper;

        public OrderService(IDapperOrderRepository dapperOrderRepo, IMapper mapper,
            ICartItemService cartItemService)
        {
            _dapperOrderRepo = dapperOrderRepo;
            _cartItemService = cartItemService;
            _mapper = mapper;
        }
        public async Task<OrderReadDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            if (id == 0)
            {
                return null;
            }
            var order = await _dapperOrderRepo.GetByIdAsync(id, cancellationToken);

            if (order == null)
            {
                return null;
            }
            
            return new OrderReadDto()
            {
                Id = order.Id,
                Email = order.Email,
                Name = order.Name,
                Address = order.Address,
                OrderDate = order.OrderDate,
                Total = order.Total,
                CartItems = _mapper.Map<List<CartItemReadDto>>( await _cartItemService
                    .GetCartItemListByOrderIdAsync(order.Id,
                     cancellationToken))
            };
        }

        public async Task<IEnumerable<OrderReadDto>> GetListAsync(CancellationToken
            cancellationToken)
        {
            var orders = await _dapperOrderRepo.GetListAsync(cancellationToken);

            List<OrderReadDto> orderListDto = new();

            foreach (var order in orders)
            {
                var orderDto = new OrderReadDto
                {
                    Id = order.Id,
                    Email = order.Email,
                    Name = order.Name,
                    Address = order.Address,
                    OrderDate = order.OrderDate,
                    Total = order.Total,
                    CartItems = _mapper.Map<List<CartItemReadDto>>(
                        await _cartItemService.GetCartItemListByOrderIdAsync(order.Id,
                        cancellationToken))
                };

                orderListDto.Add(orderDto);              
            }
            return orderListDto;
        }
    }
}

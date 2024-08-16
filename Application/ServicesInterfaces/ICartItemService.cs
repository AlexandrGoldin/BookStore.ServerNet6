using Application.ApplicationDTOs;
using Domain.Entities;

namespace Application.ServicesInterfaces
{
    public interface ICartItemService
    {
        Task<List<CartItem>?> GetCartItemListByOrderIdAsync(int id,
            CancellationToken cancellationToken);
    }
}

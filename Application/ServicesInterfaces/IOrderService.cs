using Application.ApplicationDTOs;

namespace Application.ServicesInterfaces
{
    public interface IOrderService
    {
        Task<OrderReadDto?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<OrderReadDto>> GetListAsync(CancellationToken cancellationToken);
    }
}

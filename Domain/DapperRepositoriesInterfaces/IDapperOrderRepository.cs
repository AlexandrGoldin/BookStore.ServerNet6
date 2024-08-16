using Domain.Entities;

namespace Domain.DapperRepositoriesInterfaces
{
    public interface IDapperOrderRepository
    {
        Task<IEnumerable<Order>> GetListAsync(CancellationToken cancellationToken);
        Task<Order?> GetByIdAsync(int id, CancellationToken cancellationToken);
    }
}

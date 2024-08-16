using Domain.Entities;

namespace Domain.DapperRepositoriesInterfaces
{
    public interface IDapperProductRepository
    {
        Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken);
    }
}

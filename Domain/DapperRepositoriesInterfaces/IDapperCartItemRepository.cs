using Domain.Entities;

namespace Domain.DapperRepositoriesInterfaces
{
    public interface IDapperCartItemRepository
    {
        Task<IEnumerable<CartItem>?> GetAllAsync
            (CancellationToken cancellationToken);
        Task<IEnumerable<CartItem>?> GetCartItemListByOrderIdAsync
            (int id, CancellationToken cancellationToken);      
    }
}

using Application.ApplicationDTOs;

namespace Application.ServicesInterfaces
{
    public interface IProductService
    {
        Task<ProductReadDto?> GetByIdAsync(int id, CancellationToken cancellationToken);
    }
}

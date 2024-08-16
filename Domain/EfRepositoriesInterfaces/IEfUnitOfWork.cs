namespace Domain.EfRepositoriesInterfaces
{
    public interface IEfUnitOfWork : IDisposable
    {
        IEfProductRepository Products { get; }
        IEfOrderRepository Orders { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

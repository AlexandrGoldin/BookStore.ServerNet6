namespace Domain.EfRepositoriesInterfaces
{
    public interface IEfGenericRepository<T> where T : class
    {      
        Task<T> CreateAsync(T entity, CancellationToken cancellationToken);

        int Remove(int id, CancellationToken cancellationToken);
    }
}
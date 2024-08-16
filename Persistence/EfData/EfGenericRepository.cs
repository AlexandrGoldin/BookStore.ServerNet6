using Domain.EfRepositoriesInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence.EfData
{
    public class EfGenericRepository<T> : IEfGenericRepository<T> where T : class
    {
        protected EfBookStoreContext _context;
        protected DbSet<T> dbSet;
        public EfGenericRepository(EfBookStoreContext context)
        {
            _context = context;
            dbSet = _context.Set<T>();
        }

        public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken)
        {
            await dbSet.AddAsync(entity);
            return entity;
        }

        public int Remove(int id, CancellationToken cancellationToken)
        {
            T? entity = dbSet.Find(id);
            if (entity == null)
            {
                return 0;
            };
            dbSet.Remove(entity);
            return id;          
        }
    }
}

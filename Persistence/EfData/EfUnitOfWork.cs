using Domain.EfRepositoriesInterfaces;

namespace Persistence.EfData
{
    public class EfUnitOfWork : IEfUnitOfWork
    {
        private readonly EfBookStoreContext _context;
        public IEfProductRepository Products { get; private set; } 
        public IEfOrderRepository Orders { get; private set; }

        public EfUnitOfWork(EfBookStoreContext context)
        {
            _context = context;
            Products = new EfProductRepository(_context);
            Orders = new EfOrderRepository(_context);
        }

        public async Task <int> SaveChangesAsync(CancellationToken cancellationToken)
            => await _context.SaveChangesAsync();
       
        public void Dispose() => _context.Dispose();
    }
}

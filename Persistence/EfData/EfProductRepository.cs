using Domain.Entities;
using Domain.EfRepositoriesInterfaces;
using Microsoft.Extensions.Logging;

namespace Persistence.EfData
{
    public class EfProductRepository : EfGenericRepository<Product>, IEfProductRepository
    {
        public EfProductRepository(EfBookStoreContext context) :
            base(context)
        {
        }
    }
}

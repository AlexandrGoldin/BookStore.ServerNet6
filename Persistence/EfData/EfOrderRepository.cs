using Domain.EfRepositoriesInterfaces;
using Domain.Entities;

namespace Persistence.EfData
{
    public class EfOrderRepository : EfGenericRepository<Order>, IEfOrderRepository
    {
        public EfOrderRepository(EfBookStoreContext context) :
            base(context)
        {
        }
    }   
}

using Dapper;
using Domain.DapperRepositoriesInterfaces;
using Domain.Entities;
using System.Data;

namespace Persistence.DapperData
{
    public class DapperOrderRepository : IDapperOrderRepository
    {
        private readonly IDapperDbConnection _dapperDbConnection;
        public DapperOrderRepository(IDapperDbConnection dapperDbConnection) 
            => _dapperDbConnection = dapperDbConnection;


        public async Task<Order?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var query = "select * from Orders where Id=@id";

            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                var order = await db.QueryFirstOrDefaultAsync<Order>(query, new { id });
               
                return order == null ? null : order;
            }
        }

        public async Task<IEnumerable<Order>> GetListAsync(CancellationToken cancellationToken)
        {
            var query = "SELECT * FROM Orders";
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                return await db.QueryAsync<Order>(query);
            }
        }       
    }
}

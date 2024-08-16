using Dapper;
using Domain.DapperRepositoriesInterfaces;
using Domain.Entities;
using System.Data;

namespace Persistence.DapperData
{
    public class DapperProductRepository : IDapperProductRepository
    {
        private readonly IDapperDbConnection _dapperDbConnection;
        public DapperProductRepository(IDapperDbConnection dapperDbConnection)
        {
            _dapperDbConnection = dapperDbConnection;
        }

        public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken)
        {
            var query = "SELECT * FROM Products";
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                return await db.QueryAsync<Product>(query);
            }
        }

        public async Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            if (id != 0)
            {
                var query = "SELECT * FROM Products WHERE Id = @Id";

                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    return await db.QueryFirstOrDefaultAsync<Product>(query, new { Id = id });
                }
            }
            return null;
        }
    }
}

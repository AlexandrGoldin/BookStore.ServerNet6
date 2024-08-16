using Domain.DapperRepositoriesInterfaces;
using Domain.Entities;
using Dapper;
using System.Data;

namespace Persistence.DapperData
{
    public class DapperCartItemRepository : IDapperCartItemRepository
    {
        private readonly IDapperDbConnection _dapperDbConnection;
        public DapperCartItemRepository(IDapperDbConnection dapperDbConnection)
            => _dapperDbConnection = dapperDbConnection;

        public async Task<IEnumerable<CartItem>?> GetAllAsync(CancellationToken cancellationToken)
        {
            var query = "SELECT * FROM CartItems";
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                return await db.QueryAsync<CartItem>(query);
            }
        }

        public async Task<IEnumerable<CartItem>?> GetCartItemListByOrderIdAsync
            (int id, CancellationToken cancellationToken)
        {
            var query = $@"
                  select c.Count as count
                  , p.Title as title                
                  , p.Price                 
                  from CartItems c
                  left join Products p on p.Id = c.ProductId
                  where c.OrderId = {id}             
            ";

            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                var data = await db.QueryAsync<CartItem, Product, CartItem>(query, (c, p) =>
                {
                    c.Product = p;
                    return c;
                }, splitOn: "count, title");

                return data == null ? null : data.ToList();
            }          
        }
    }
}

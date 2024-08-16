using System.Data;

namespace Domain.DapperRepositoriesInterfaces
{
    public interface IDapperDbConnection
    {
        public IDbConnection CreateConnection();
    }
}

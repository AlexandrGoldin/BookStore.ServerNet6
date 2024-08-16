using Domain.DapperRepositoriesInterfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Persistence.DapperData
{
    public class DapperDbConnection : IDapperDbConnection
    {
        private readonly IConfiguration? _configuration;
        private readonly string? _connectionString;

        public DapperDbConnection() { }

        public DapperDbConnection(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("BookStoreDb") ??
                throw new ApplicationException("Connection string is missing");
        }

        public IDbConnection CreateConnection()       
            => new SqlConnection(_connectionString);       
    }
}

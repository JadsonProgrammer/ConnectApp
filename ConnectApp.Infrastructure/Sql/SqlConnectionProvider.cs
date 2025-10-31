using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ConnectApp.Infrastructure.Sql
{
    public class SqlConnectionProvider : IConnectionProvider
    {
        private readonly string _connectionString;

        public SqlConnectionProvider(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }


    }
}

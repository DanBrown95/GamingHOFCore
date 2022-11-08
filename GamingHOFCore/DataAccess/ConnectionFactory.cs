using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace GamingHOFCore.DataAccess
{
    public class ConnectionFactory
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public ConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("LocalConnection");
        }

        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
}

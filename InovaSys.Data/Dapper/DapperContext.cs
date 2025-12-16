using Inovasys.Data.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Inovasys.Data.Dapper
{
    public class DapperContext : IDapperContext
    {
        private readonly string connectionString;
        public DapperContext(IConfiguration config)
        {
            connectionString = config.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException("Connection string is null! Plese add your connection string in user secrets!");
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(connectionString);

        }
    }
}

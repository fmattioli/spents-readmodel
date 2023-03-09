using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Spents.ReadModel.Infrastructure.Data
{
    public class DbSession : IDisposable
    {
        public IDbConnection Connection { get; private set; }

        public DbSession(IConfiguration configuration)
        {
            Connection = new SqlConnection(configuration.GetConnectionString("SqlSettings:ConnectionString"));
            Connection.Open();
        }

        public void Dispose() => Connection?.Dispose();
    }
}

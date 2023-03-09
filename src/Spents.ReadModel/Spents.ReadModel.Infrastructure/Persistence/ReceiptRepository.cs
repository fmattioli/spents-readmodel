using Spents.ReadModel.Domain.Entities;
using Spents.ReadModel.Domain.Interfaces;
using Spents.ReadModel.Infrastructure.Data;

using System.Data.SqlClient;

namespace Spents.ReadModel.Infrastructure.Persistence
{
    public class ReceiptRepository : IReceiptRepository
    {
        private DbSession _db;
        public ReceiptRepository(DbSession dbSession)
        {
            this._db = dbSession;
        }
        public async Task AddReceipt(ReceiptEntity? receipt)
        {
            using(var conn = _db.Connection)
            {
                
            }
        }
    }
}

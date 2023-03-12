using Dapper;
using Spents.Domain.ValueObjects;
using Spents.ReadModel.Domain.Entities;
using Spents.ReadModel.Domain.Interfaces;
using Spents.ReadModel.Infrastructure.Constants;
using Spents.ReadModel.Infrastructure.Data;

namespace Spents.ReadModel.Infrastructure.Persistence
{
    public class ReceiptRepository : IReceiptRepository
    {
        private DbSession _db;

        public ReceiptRepository(DbSession dbSession) => this._db = dbSession;

        public async Task AddReceipt(ReceiptEntity receipt)
        {
            using (var conn = _db.Connection)
            {
                var pk_Receipt = await conn.QuerySingleAsync<int>(
                    SqlCommands.InsertReceipt,
                    new
                    {
                        ReceiptId = receipt.Id,
                        EstablishmentName = receipt.EstablishmentName,
                        ReceiptDate = receipt.ReceiptDate,
                    });

                await AddReceiptItems(pk_Receipt, receipt.ReceiptItems);
            }
        }

        private async Task AddReceiptItems(int pk_Receipt, IEnumerable<ReceiptItem> receiptItems)
        {
            using (var conn = _db.Connection)
            {
                await Task.WhenAll(receiptItems.Select(async x =>
                await conn.ExecuteAsync(SqlCommands.InsertReceiptItems, new
                {
                    FK_Receipt_Id = pk_Receipt,
                    ReceiptItemId = x.Id,
                    ItemName = x.ItemName,
                    Quantity = x.Quantity,
                    ItemPrice = x.ItemPrice,
                    Observation = x.Observation
                })));
            }

        }
    }
}

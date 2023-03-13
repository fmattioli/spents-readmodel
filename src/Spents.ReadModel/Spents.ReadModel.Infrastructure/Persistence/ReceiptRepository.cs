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
        private readonly DbSession _db;

        public ReceiptRepository(DbSession dbSession) => this._db = dbSession;

        public async Task AddReceiptAsync(ReceiptEntity receipt)
        {
            using var conn = _db.Connection;
            var pk_Receipt = await conn.QuerySingleAsync<int>(
                SqlCommands.InsertReceipt,
                new
                {
                    ReceiptId = receipt.Id,
                    receipt.EstablishmentName,
                    receipt.ReceiptDate,
                });

            await AddReceiptItemsAsync(pk_Receipt, receipt.ReceiptItems);
        }

        private async Task AddReceiptItemsAsync(int pk_Receipt, IEnumerable<ReceiptItem> receiptItems)
        {
            using var conn = _db.Connection;
            await Task.WhenAll(receiptItems.Select(async x =>
            await conn.ExecuteAsync(SqlCommands.InsertReceiptItems, new
            {
                FK_Receipt_Id = pk_Receipt,
                ReceiptItemId = x.Id,
                x.ItemName,
                x.Quantity,
                x.ItemPrice,
                x.Observation
            })));

        }
    }
}

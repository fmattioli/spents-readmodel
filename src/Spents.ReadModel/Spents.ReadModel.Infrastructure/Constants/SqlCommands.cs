using System.Text;

namespace Spents.ReadModel.Infrastructure.Constants
{
    public static class SqlCommands
    {
        private static StringBuilder query = new StringBuilder();
        public static string InsertReceipt
        {
            get
            {
                query.Clear();
                query.AppendLine(" INSERT INTO [Receipts]");
                query.AppendLine("  (  ");
                query.AppendLine("  [ReceiptId], ");
                query.AppendLine("  [EstablishmentName], ");
                query.AppendLine("  [ReceiptDate] ");
                query.AppendLine("  ) ");
                query.AppendLine(" VALUES ");
                query.AppendLine(" ( ");
                query.AppendLine("  @ReceiptId,  ");
                query.AppendLine("  @EstablishmentName,  ");
                query.AppendLine("  @ReceiptDate  ");
                query.AppendLine("  )  ");
                query.AppendLine("  SELECT CAST(SCOPE_IDENTITY() as int)  ");
                return query.ToString();
            }
        }

        public static string InsertReceiptItems
        {
            get
            {
                query.Clear();
                query.AppendLine(" INSERT INTO [ReceiptItems]");
                query.AppendLine("  (  ");
                query.AppendLine("  [FK_Receipt_Id], ");
                query.AppendLine("  [ReceiptItemId], ");
                query.AppendLine("  [ItemName], ");
                query.AppendLine("  [Quantity], ");
                query.AppendLine("  [ItemPrice], ");
                query.AppendLine("  [Observation] ");
                query.AppendLine("  ) ");
                query.AppendLine(" VALUES ");
                query.AppendLine(" ( ");
                query.AppendLine("  @FK_Receipt_Id,  ");
                query.AppendLine("  @ReceiptItemId,  ");
                query.AppendLine("  @ItemName,  ");
                query.AppendLine("  @Quantity,  ");
                query.AppendLine("  @ItemPrice,  ");
                query.AppendLine("  @Observation  ");
                query.AppendLine("  )  ");
                return query.ToString();
            }
        }
    }
}

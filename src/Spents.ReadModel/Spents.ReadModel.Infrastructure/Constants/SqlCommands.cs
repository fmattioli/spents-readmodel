using System.Text;

namespace Spents.ReadModel.Infrastructure.Constants
{
    public static class SqlCommands
    {
        private static StringBuilder query = new StringBuilder();
        private static StringBuilder InsertReceipt
        {
            get
            {
                query.Clear();
                query.Append(" INSERT INTO ");
                query.Append("  MyTable  ");
                query.Append("  (column1, column2)  ");
                query.Append(" VALUES ");
                query.Append("  (@val1 , @val2) ");

                return query;
            }
        }
    }
}

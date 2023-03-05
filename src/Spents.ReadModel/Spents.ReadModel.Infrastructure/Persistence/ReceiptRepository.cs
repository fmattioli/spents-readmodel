using Spents.ReadModel.Domain.Entities;
using Spents.ReadModel.Domain.Interfaces;

namespace Spents.ReadModel.Infrastructure.Persistence
{
    public class ReceiptRepository : IReceiptRepository
    {
        public Task AddReceipt(ReceiptEntity? receipt)
        {
            throw new NotImplementedException();
        }
    }
}

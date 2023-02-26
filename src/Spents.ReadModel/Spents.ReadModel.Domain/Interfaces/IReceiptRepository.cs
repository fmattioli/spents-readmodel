using Spents.ReadModel.Domain.Entities;

namespace Spents.ReadModel.Domain.Interfaces
{
    public interface IReceiptRepository
    {
        Task AddReceipt(ReceiptEntity? receipt);
    }
}

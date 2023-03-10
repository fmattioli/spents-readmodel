using Spents.ReadModel.Domain.Entities;

namespace Spents.ReadModel.Domain.Interfaces
{
    public interface IReceiptRepository
    {
        Task AddReceiptAsync(ReceiptEntity receipt);

        Task<IReadOnlyCollection<ReceiptEntity>> GetReceipts(params string[] filters);
    }
}

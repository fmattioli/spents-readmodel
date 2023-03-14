using MediatR;
using Spents.Domain.Entities;
using Spents.ReadModel.Domain.Interfaces;

namespace Spents.ReadModel.Application.Queries
{
    public class GetReceiptsQueryHandler : IRequestHandler<GetReceiptsQuery, IReadOnlyCollection<Receipt>>
    {
        private readonly IReceiptRepository _receiptRepository;
        public GetReceiptsQueryHandler(IReceiptRepository receiptRepository)
        {
            this._receiptRepository = receiptRepository;
        }

        public async Task<IReadOnlyCollection<Receipt>> Handle(GetReceiptsQuery request, CancellationToken cancellationToken)
        {
            var filters = new List<string>
            {
                MatchByReceiptIds(request.getReceiptsViewModel)
            };

            return await _receiptRepository.GetReceipts(filters
                .Where(x => string.IsNullOrEmpty(x))
                .ToArray()
                );
        }

        private string MatchByReceiptIds(GetReceiptsViewRequest getReceiptsViewRequest)
        {
            return getReceiptsViewRequest.ReceiptIds.Any()
            ? string.Join(",", getReceiptsViewRequest.ReceiptIds.Select(x => x.ToString()))
            : string.Empty;
        }
    }
}

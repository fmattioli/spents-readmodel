using MediatR;
using Spents.Domain.Entities;

namespace Spents.ReadModel.Application.Queries
{
    public class GetReceiptsQueryHandler : IRequestHandler<GetReceiptsQuery, IReadOnlyCollection<Receipt>>
    {
        public async Task<IReadOnlyCollection<Receipt>> Handle(GetReceiptsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

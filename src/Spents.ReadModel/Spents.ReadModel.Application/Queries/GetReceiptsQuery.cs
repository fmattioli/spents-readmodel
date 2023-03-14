using MediatR;
using Spents.Domain.Entities;

namespace Spents.ReadModel.Application.Queries
{
    public record GetReceiptsQuery(GetReceiptsViewRequest getReceiptsViewModel) : IRequest<IReadOnlyCollection<Receipt>>;
}

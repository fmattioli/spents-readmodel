using MediatR;
using Spents.Domain.Entities;

namespace Spents.ReadModel.Application.Queries
{
    public record GetReceiptsQuery(GetReceiptsViewModel getReceiptsViewModel) : IRequest<IReadOnlyCollection<Receipt>>;
}

using Microsoft.AspNetCore.Mvc;

namespace Spents.ReadModel.Application.Queries
{
    public class GetReceiptsViewRequest
    {
        [FromQuery]
        public IEnumerable<Guid> ReceiptIds { get; set; } = null!;
        [FromQuery]
        public IEnumerable<string> EstablishmentNames { get; set; } = null!;
        [FromQuery]
        public DateTime ReceiptDate { get; set; }
        [FromQuery]
        public DateTime ReceiptDateFinal { get; set; }
    }
}

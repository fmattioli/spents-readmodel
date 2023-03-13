namespace Spents.ReadModel.Application.Queries
{
    public class GetReceiptsViewModel
    {
        public IEnumerable<Guid> ReceiptIds { get; set; } = null!;
        public IEnumerable<string> EstablishmentNames { get; set; } = null!;
        public DateTime ReceiptDate { get; set; }
        public DateTime ReceiptDateFinal { get; set; }
    }
}

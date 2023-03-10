using KafkaFlow;
using KafkaFlow.TypedHandler;
using Serilog;
using Spents.Domain.Entities;
using Spents.Events.v1;
using Spents.ReadModel.Domain.Entities;
using Spents.ReadModel.Domain.Interfaces;

namespace Spents.ReadModel.Application.Kafka.Handlers
{
    public class ReceiptCreatedHandler : IMessageHandler<ReceiptEvent<Receipt>>
    {
        private readonly ILogger log;
        private readonly IReceiptRepository _receiptRepository;
        public ReceiptCreatedHandler(ILogger log, IReceiptRepository receiptRepository)
        {
            this.log = log;
            this._receiptRepository = receiptRepository;
        }

        public async Task Handle(IMessageContext context, ReceiptEvent<Receipt> message)
        {
            if (message is not null && message.Body != null)
            {
                 await _receiptRepository.AddReceiptAsync(new ReceiptEntity
                {
                    Id = message.Body.Id,
                    EstablishmentName = message.Body.EstablishmentName,
                    ReceiptDate = message.Body.ReceiptDate,
                    ReceiptItems = message.Body.ReceiptItems,
                });

                this.log.Information(
                    $"Receipt created with successfully.",
                    () => new
                    {
                        message.Body
                    });
            }
        }
    }
}

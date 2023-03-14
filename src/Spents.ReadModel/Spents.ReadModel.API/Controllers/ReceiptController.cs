using MediatR;
using Microsoft.AspNetCore.Mvc;
using Spents.ReadModel.Application.Queries;

namespace Spents.ReadModel.API.Controllers
{
    [Route("api/readmodel")] 
    [ApiController]
    public class ReceiptController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReceiptController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// GET receipts based on pre determined filters.
        /// Required at least a filter as parameters.
        /// </summary>
        /// <returns>Return a list of receipts based on pre determined filters.</returns>
        [HttpGet]
        [Route("/getReceipts", Name = nameof(ReceiptController.GetReceipts))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetReceipts([FromRoute] GetReceiptsViewRequest getReceiptsViewModel)
        {
            var receiptId = await _mediator.Send(new GetReceiptsQuery(getReceiptsViewModel));
            return Created("/addReceipt", receiptId);
        }
    }
}

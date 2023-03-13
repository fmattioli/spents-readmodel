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
        /// Admin endpoint made to Get all products in ProductProjection based on product id OR product number.
        /// Required query string parameters.
        /// </summary>
        /// <returns>Return a list of product presents in ProductProjection.</returns>
        [HttpGet]
        [Route("/getReceipts", Name = nameof(ReceiptController.GetReceipts))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetReceipts([FromQuery] GetReceiptsViewModel getReceiptsViewModel)
        {
            var receiptId = await _mediator.Send(new GetReceiptsQuery(getReceiptsViewModel));
            return Created("/addReceipt", receiptId);
        }
    }
}

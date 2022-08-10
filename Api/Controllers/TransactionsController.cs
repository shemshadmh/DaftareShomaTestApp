using System.Globalization;
using Application.Transactions.Commands.CreateTransaction;
using Application.Transactions.Queries.GetReportTransaction;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("report")]
        public async Task<IActionResult> Get()
        {
            //var test = DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("en-US", true));
            var result = await _mediator.Send(new GetReportTransactionQuery());
            return this.Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateTransactionCommand createTransactionCommand)
        {
            await _mediator.Send(createTransactionCommand);
            return Ok();
        }
    }
}

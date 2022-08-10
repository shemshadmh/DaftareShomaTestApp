using Application.Persons.Commands.CreatePerson;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PersonsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreatePersonCommand createPersonCommand)
        {
            await _mediator.Send(createPersonCommand);
             return Ok();
        }
    }
}

using MediatR;

using Microsoft.AspNetCore.Mvc;

using TeamCounters.Application.Counters.Commands.CreateCounter;
using TeamCounters.Application.Counters.Queries.GetCounterById;

namespace TeamCounters.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountersController : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CounterDto>> GetById([FromServices] ISender sender, Guid id)
    {
        var counter = await sender.Send(new GetCounterByIdQuery(id));
        return Ok(counter);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateCounter(
        [FromServices] ISender sender,
        [FromBody] CreateCounterCommand command)
    {
        var id = await sender.Send(command);
        return Ok(id);
    }
}

using MediatR;

using Microsoft.AspNetCore.Mvc;

using TeamCounters.Application.Counters.Commands.CreateCounter;
using TeamCounters.Application.Counters.Commands.IncrementCounter;
using TeamCounters.Application.Counters.Queries.GetCounterById;

namespace TeamCounters.Web.Api.Controllers;

/// <summary>
/// Process requests to manage counters.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class CountersController : ControllerBase
{
    /// <summary>
    /// Returns a counter by its identifier
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="id"></param>
    /// <response code="200">The counter's data</response>
    /// <response code="404">The counter was not found</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CounterDto>> GetById([FromServices] ISender sender, Guid id)
    {
        var counter = await sender.Send(new GetCounterByIdQuery(id));
        return Ok(counter);
    }

    /// <summary>
    /// Creates a new counter
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="command"></param>
    /// <returns>A unique identifier of the newly created counter</returns>
    /// <response code="201">The counter was created successfully</response>
    /// <response code="400">One or more errors have occured during counter creation, e.g. validation errors</response>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromServices] ISender sender,
        [FromBody] CreateCounterCommand command)
    {
        var id = await sender.Send(command);

        // TODO: In some cases it may be more useful to return more information about created counter than only its id
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    /// <summary>
    /// Increments value of the counter by the specified amount
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="id"></param>
    /// <param name="command"></param>
    /// <returns>Total count accumulated by the counter</returns>
    /// <response code="200">The counter's value was incremented successfully</response>
    /// <response code="400"></response>
    /// <response code="404">The counter was not found</response>
    [HttpPatch("{id:guid}/increment")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<int>> Increment(
        [FromServices] ISender sender,
        Guid id,
        [FromBody] IncrementCounterCommand command)
    {
        if (id != command.CounterId)
        {
            return BadRequest("Value of counter id in the request path should be equal to counterId value in the request body");
        }

        var totalCount = await sender.Send(command);

        return Ok(totalCount);
    }
}

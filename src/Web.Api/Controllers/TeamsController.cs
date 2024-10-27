using MediatR;

using Microsoft.AspNetCore.Mvc;

using TeamCounters.Application.Teams.Commands.AddCounter;
using TeamCounters.Application.Teams.Commands.CreateTeam;
using TeamCounters.Application.Teams.Commands.DeleteTeam;
using TeamCounters.Application.Teams.Commands.RemoveCounter;
using TeamCounters.Application.Teams.Queries.GetTeamById;
using TeamCounters.Application.Teams.Queries.GetTeamCounters;
using TeamCounters.Application.Teams.Queries.GetTeams;

namespace TeamCounters.Web.Api.Controllers;

/// <summary>
/// Process requests to manage teams
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class TeamsController : ControllerBase
{
    /// <summary>
    /// Returns a list of all teams
    /// </summary>
    /// <param name="sender"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<TeamBreifDto>> GetList([FromServices] ISender sender)
    {
        var teams = await sender.Send(new GetTeamsQuery());

        return Ok(teams);
    }

    /// <summary>
    /// Returns a team by its identifier with total count of the team's counters
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="200">The team with total count of its counters</response>
    /// <response code="404">The team was not found</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TeamDto>> GetById([FromServices] ISender sender, Guid id)
    {
        var team = await sender.Send(new GetTeamByIdQuery(id));

        return Ok(team);
    }

    /// <summary>
    /// Creates a new team
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="command"></param>
    /// <returns>A unique identifier of the newly created team</returns>
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromServices] ISender sender,
        [FromBody] CreateTeamCommand command)
    {
        var teamId = await sender.Send(command);

        return Ok(teamId);
    }

    /// <summary>
    /// Returns a list of counters belonging to the team with their accumulated amounts
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="id">The team's unique identifier</param>
    /// <returns></returns>
    /// <response code="200">The team with total count of its counters</response>
    /// <response code="404">The team was not found</response>
    [HttpGet("{id:guid}/counters")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<TeamCounterDto>>> GetCounters([FromServices] ISender sender, Guid id)
    {
        var counters = await sender.Send(new GetTeamCountersQuery(id));

        return Ok(counters);
    }

    /// <summary>
    /// Deletes existing team by its identifier
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="id"></param>
    /// <response code="204">The team was successfully deleted</response>
    /// <response code="404">The team with the specified identifier was not found</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete([FromServices] ISender sender, Guid id)
    {
        await sender.Send(new DeleteTeamCommand(id));

        return NoContent();
    }

    /// <summary>
    /// Adds the counter to the team
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="id">Identifier of the team to which the counter should be added</param>
    /// <param name="command"></param>
    /// <response code="204">The counter was successfully added to the team</response>
    /// <response code="400">Id value is not equal to teamId value in the request body</response>
    /// <response code="404">Either the team or the counter was not found</response>
    [HttpPost("{id:guid}/counters")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> AddCounter([FromServices] ISender sender, Guid id, [FromBody] AddCounterCommand command)
    {
        if (id != command.TeamId)
        {
            return BadRequest("Value of team id in the query should be equal to teamId value in the request body");
        }

        await sender.Send(command);

        return NoContent();
    }

    /// <summary>
    /// Removes the counter from the team
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="teamId"></param>
    /// <param name="counterId"></param>
    /// <response code="204">The counter was successfully removed from the team</response>
    /// <response code="404">Either the team was not found or the counter was not found or the counter doesn't belong to the team</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{teamId:guid}/counters/{counterId:guid}")]
    public async Task<ActionResult> RemoveCounter([FromServices] ISender sender, Guid teamId, Guid counterId)
    {
        await sender.Send(new RemoveCounterCommand(teamId, counterId));

        return NoContent();
    }
}

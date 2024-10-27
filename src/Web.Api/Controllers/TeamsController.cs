using MediatR;

using Microsoft.AspNetCore.Mvc;

using TeamCounters.Application.Teams.Commands.AddCounter;
using TeamCounters.Application.Teams.Commands.CreateTeam;
using TeamCounters.Application.Teams.Commands.DeleteTeam;
using TeamCounters.Application.Teams.Commands.RemoveCounter;
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
    /// <param name="teamId">Identifier of the team to which the counter should be added</param>
    /// <param name="counterId">Identifier of the counter which should be added to the team</param>
    /// <response code="204">The counter was successfully added to the team</response>
    /// <response code="404">Either the team or the counter was not found</response>
    [HttpPost("{teamId:guid}/counters")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> AddCounter([FromServices] ISender sender, Guid teamId, [FromBody] Guid counterId)
    {
        await sender.Send(new AddCounterCommand(teamId, counterId));

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

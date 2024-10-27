using MediatR;

using Microsoft.AspNetCore.Mvc;

using TeamCounters.Application.Teams.Commands.CreateTeam;
using TeamCounters.Application.Teams.Commands.DeleteTeam;

namespace TeamCounters.Web.Api.Controllers;

/// <summary>
/// Process requests to manage teams
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class TeamsController : ControllerBase
{
    /// <summary>
    /// Creates a new team
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="command"></param>
    /// <returns>An unique identifier of a newly created team</returns>
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
}

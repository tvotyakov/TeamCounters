using Ardalis.GuardClauses;

using MediatR;

using TeamCounters.Domain.Teams;

namespace TeamCounters.Application.Teams.Commands.DeleteTeam;
public sealed class DeleteTeamCommandHandler(ITeamsRepository teamsRepo) : IRequestHandler<DeleteTeamCommand>
{
    private readonly ITeamsRepository _teamsRepo = teamsRepo;

    public async Task Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
    {
        var team = await _teamsRepo.GetById(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, team);

        await _teamsRepo.Delete(team.Id, cancellationToken);
    }
}

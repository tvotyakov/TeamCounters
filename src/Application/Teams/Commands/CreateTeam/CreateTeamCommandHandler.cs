using MediatR;

using TeamCounters.Domain.Teams;

namespace TeamCounters.Application.Teams.Commands.CreateTeam;
public sealed class CreateTeamCommandHandler(ITeamsRepository teamsRepo) : IRequestHandler<CreateTeamCommand, Guid>
{
    private readonly ITeamsRepository _teamsRepo = teamsRepo;

    public async Task<Guid> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
    {
        var team = await _teamsRepo.Add(
            new Team { Name = request.Name },
            cancellationToken);

        return team.Id;
    }
}

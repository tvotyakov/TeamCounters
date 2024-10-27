using Ardalis.GuardClauses;

using MediatR;

using TeamCounters.Domain.Teams;

namespace TeamCounters.Application.Teams.Commands.RemoveCounter;
public sealed class RemoveCounterCommandHandler(ITeamsRepository teamsRepo) : IRequestHandler<RemoveCounterCommand>
{
    private readonly ITeamsRepository _teamsRepo = teamsRepo;

    public async Task Handle(RemoveCounterCommand request, CancellationToken cancellationToken)
    {
        var team = await _teamsRepo.GetById(request.TeamId, cancellationToken);
        Guard.Against.NotFound(request.TeamId, team);

        var counter = team.Counters.FirstOrDefault(c => c.Id == request.CounterId);
        Guard.Against.NotFound(request.CounterId, counter);

        team.RemoveCounter(counter);
    }
}

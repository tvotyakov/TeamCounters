using Ardalis.GuardClauses;

using MediatR;

using TeamCounters.Domain.Counters;
using TeamCounters.Domain.Teams;

namespace TeamCounters.Application.Teams.Commands.AddCounter;
public sealed class AddCounterCommandHandler(ITeamsRepository teamsRepo, ICountersRepository countersRepo) : IRequestHandler<AddCounterCommand>
{
    private readonly ITeamsRepository _teamsRepo = teamsRepo;
    private readonly ICountersRepository _countersRepo = countersRepo;

    public async Task Handle(AddCounterCommand request, CancellationToken cancellationToken)
    {
        var team = await _teamsRepo.GetById(request.TeamId, cancellationToken);
        Guard.Against.NotFound(request.TeamId, team);

        var counter = await _countersRepo.GetById(request.CounterId, cancellationToken);
        Guard.Against.NotFound(request.CounterId, counter);

        team.AddCounter(counter);
    }
}

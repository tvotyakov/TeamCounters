using Ardalis.GuardClauses;

using MediatR;

using TeamCounters.Domain.Teams;

namespace TeamCounters.Application.Teams.Queries.GetTeamCounters;

public sealed class GetTeamCountersQueryHandler(ITeamsRepository teamsRepo) : IRequestHandler<GetTeamCountersQuery, IEnumerable<TeamCounterDto>>
{
    private readonly ITeamsRepository _teamsRepo = teamsRepo;

    public async Task<IEnumerable<TeamCounterDto>> Handle(GetTeamCountersQuery request, CancellationToken cancellationToken)
    {
        var team = await _teamsRepo.GetById(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, team);

        return team.Counters.Select(counter => new TeamCounterDto
        {
            Id = counter.Id,
            Name = counter.Name,
            TotalCount = counter.TotalCount
        });
    }
}

using Ardalis.GuardClauses;

using MediatR;

using TeamCounters.Domain.Teams;

namespace TeamCounters.Application.Teams.Queries.GetTeamById;
public class GetTeamByIdQueryHandler(ITeamsRepository teamsRepo) : IRequestHandler<GetTeamByIdQuery, TeamDto>
{
    private readonly ITeamsRepository _teamsRepo = teamsRepo;

    public async Task<TeamDto> Handle(GetTeamByIdQuery request, CancellationToken cancellationToken)
    {
        var team = await _teamsRepo.GetById(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, team);

        return new TeamDto
        {
            Id = team.Id,
            Name = team.Name,
            TotalCounts = team.Counters.Sum(counter => counter.TotalCount)
        };
    }
}

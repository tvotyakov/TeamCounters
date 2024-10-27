using MediatR;

using TeamCounters.Domain.Teams;

namespace TeamCounters.Application.Teams.Queries.GetTeams;
public class GetTeamsQueryHandler(ITeamsRepository teamsRepo) : IRequestHandler<GetTeamsQuery, IEnumerable<TeamBreifDto>>
{
    private readonly ITeamsRepository _teamsRepo = teamsRepo;

    public async Task<IEnumerable<TeamBreifDto>> Handle(GetTeamsQuery request, CancellationToken cancellationToken)
    {
        return (await _teamsRepo.GetList())
            .Select(team => new TeamBreifDto
            {
                Id = team.Id,
                Name = team.Name,
                TotalCount = team.Counters.Sum(counter => counter.TotalCount)
            });
    }
}

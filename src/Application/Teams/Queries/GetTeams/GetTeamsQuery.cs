using MediatR;

namespace TeamCounters.Application.Teams.Queries.GetTeams;

public sealed record GetTeamsQuery : IRequest<IEnumerable<TeamBreifDto>>;

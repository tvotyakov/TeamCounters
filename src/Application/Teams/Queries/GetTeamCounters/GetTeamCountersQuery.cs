using MediatR;

namespace TeamCounters.Application.Teams.Queries.GetTeamCounters;
public sealed record GetTeamCountersQuery(Guid Id) : IRequest<IEnumerable<TeamCounterDto>>;
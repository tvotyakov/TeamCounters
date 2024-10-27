using MediatR;

namespace TeamCounters.Application.Teams.Queries.GetTeamById;
public sealed record GetTeamByIdQuery(Guid Id) : IRequest<TeamDto>;

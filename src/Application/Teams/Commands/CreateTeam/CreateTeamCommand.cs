using MediatR;

namespace TeamCounters.Application.Teams.Commands.CreateTeam;
public sealed record CreateTeamCommand(string Name) : IRequest<Guid>;

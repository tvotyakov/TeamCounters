using MediatR;

namespace TeamCounters.Application.Teams.Commands.DeleteTeam;
public sealed record DeleteTeamCommand(Guid Id) : IRequest;

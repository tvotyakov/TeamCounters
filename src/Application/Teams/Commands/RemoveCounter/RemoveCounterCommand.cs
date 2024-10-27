using MediatR;

namespace TeamCounters.Application.Teams.Commands.RemoveCounter;

public sealed record RemoveCounterCommand(Guid TeamId, Guid CounterId) : IRequest;

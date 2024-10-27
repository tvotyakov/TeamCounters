using MediatR;

namespace TeamCounters.Application.Teams.Commands.AddCounter;

public sealed record AddCounterCommand(Guid TeamId, Guid CounterId) : IRequest;

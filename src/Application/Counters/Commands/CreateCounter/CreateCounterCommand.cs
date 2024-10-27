using MediatR;

namespace TeamCounters.Application.Counters.Commands.CreateCounter;
public sealed record CreateCounterCommand(string Name) : IRequest<Guid>;
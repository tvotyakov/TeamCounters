using MediatR;

namespace TeamCounters.Application.Counters.Commands.IncrementCounter;

public sealed record IncrementCounterCommand(Guid CounterId, int Value) : IRequest<int>;

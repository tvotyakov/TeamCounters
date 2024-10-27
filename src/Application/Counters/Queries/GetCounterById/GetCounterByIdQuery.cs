using MediatR;

namespace TeamCounters.Application.Counters.Queries.GetCounterById;
public sealed record GetCounterByIdQuery(Guid Id) : IRequest<CounterDto>;
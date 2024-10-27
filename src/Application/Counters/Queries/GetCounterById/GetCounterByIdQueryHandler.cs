using Ardalis.GuardClauses;

using MediatR;

using TeamCounters.Domain.Counters;

namespace TeamCounters.Application.Counters.Queries.GetCounterById;
public sealed class GetCounterByIdQueryHandler(ICountersRepository countersRepo) : IRequestHandler<GetCounterByIdQuery, CounterDto>
{
    private readonly ICountersRepository _countersRepo = countersRepo;

    public async Task<CounterDto> Handle(GetCounterByIdQuery request, CancellationToken cancellationToken)
    {
        var counter = await _countersRepo.GetById(request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, counter);

        return new CounterDto
        {
            Id = counter.Id,
            Name = counter.Name,
            TotalCount = counter.TotalCount,
        };
    }
}

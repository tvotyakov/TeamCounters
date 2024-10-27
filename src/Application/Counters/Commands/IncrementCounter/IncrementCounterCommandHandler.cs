using Ardalis.GuardClauses;

using MediatR;

using TeamCounters.Domain.Counters;

namespace TeamCounters.Application.Counters.Commands.IncrementCounter;
public sealed class IncrementCounterCommandHandler(ICountersRepository countersRepo) : IRequestHandler<IncrementCounterCommand, int>
{
    private readonly ICountersRepository _countersRepo = countersRepo;

    public async Task<int> Handle(IncrementCounterCommand request, CancellationToken cancellationToken)
    {
        Guard.Against.NegativeOrZero(request.Value, nameof(request.Value));

        var counter = await _countersRepo.GetById(request.CounterId, cancellationToken);
        Guard.Against.NotFound(request.CounterId, counter);

        counter.Increment(request.Value);

        return counter.TotalCount;
    }
}

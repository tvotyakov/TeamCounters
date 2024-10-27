using MediatR;

using TeamCounters.Domain.Counters;

namespace TeamCounters.Application.Counters.Commands.CreateCounter;
public sealed class CreateCounterCommandHandler(ICountersRepository countersRepo) : IRequestHandler<CreateCounterCommand, Guid>
{
    private readonly ICountersRepository _countersRepo = countersRepo;

    public async Task<Guid> Handle(CreateCounterCommand request, CancellationToken cancellationToken)
    {
        var counter = await _countersRepo.Add(
            new Counter { Name = request.Name },
            cancellationToken);

        return counter.Id;
    }
}

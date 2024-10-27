namespace TeamCounters.Domain.Counters;
public interface ICountersRepository
{
    ValueTask<Counter> Add(Counter counter, CancellationToken cancellationToken = default);

    ValueTask<Counter?> GetById(Guid id, CancellationToken cancellationToken = default);
}

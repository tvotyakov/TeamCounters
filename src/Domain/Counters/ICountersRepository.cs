namespace TeamCounters.Domain.Counters;
public interface ICountersRepository
{
    public ValueTask<Counter> Add(Counter counter, CancellationToken cancellationToken = default);

    public ValueTask<Counter?> GetById(Guid id, CancellationToken cancellationToken = default);
}

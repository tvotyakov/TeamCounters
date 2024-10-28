using TeamCounters.Domain.Counters;

namespace TeamCounters.DataAccess.InMemory.Counters;
public sealed class CountersRepository : ICountersRepository
{
    public ValueTask<Counter> Add(Counter counter, CancellationToken cancellationToken = default)
    {
        counter.Id = Guid.NewGuid();

        if (!Db.Counters.TryAdd(counter.Id, counter))
        {
            throw new ApplicationException("Counter has not been added");
        }

        return ValueTask.FromResult(counter);
    }

    public ValueTask<Counter?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        Db.Counters.TryGetValue(id, out var counter);

        return ValueTask.FromResult(counter);
    }

    public ValueTask<bool> IsExistByName(string name)
    {
        var isExist = Db.Counters
            .Values
            .Any(x => x.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));

        return ValueTask.FromResult(isExist);
    }
}

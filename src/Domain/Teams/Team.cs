using TeamCounters.Domain.Abstract;
using TeamCounters.Domain.Counters;

namespace TeamCounters.Domain.Teams;
public class Team : BaseEntity<Guid>
{
    private readonly List<Counter> _counters = [];

    public required string Name { get; init; }

    public IReadOnlyCollection<Counter> Counters { get => _counters; }

    public void AddCounter(Counter counter)
    {
        if (counter.Team is not null)
        {
            throw new InvalidOperationException("Counter already belongs to a team");
        }

        _counters.Add(counter);
        counter.Team = this;
    }

    public void RemoveCounter(Counter counter)
    {
        _counters.Remove(counter);
        counter.Team = null;
    }
}

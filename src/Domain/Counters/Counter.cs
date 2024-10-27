using TeamCounters.Domain.Abstract;
using TeamCounters.Domain.Teams;

namespace TeamCounters.Domain.Counters;

public class Counter : BaseEntity<Guid>
{
    public required string Name { get; init; }

    public Team? Team { get; set; }

    public int TotalCount { get; private set; }
}

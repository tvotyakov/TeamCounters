using TeamCounters.Domain.Abstract;

namespace TeamCounters.Domain.Counters;
public class Counter : BaseEntity<Guid>
{
    public required string Name { get; init; }

    public int TotalCount { get; private set; }
}

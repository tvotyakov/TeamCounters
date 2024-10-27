using System.Collections.Concurrent;

using TeamCounters.Domain.Counters;

namespace TeamCounters.DataAccess.InMemory;
internal static class Db
{
    public static ConcurrentDictionary<Guid, Counter> Counters { get; } = new();
}

using System.Collections.Concurrent;

using TeamCounters.Domain.Counters;
using TeamCounters.Domain.Teams;

namespace TeamCounters.DataAccess.InMemory;
internal static class Db
{
    public static ConcurrentDictionary<Guid, Counter> Counters { get; } = new();

    public static ConcurrentDictionary<Guid, Team> Teams { get; } = new();
}

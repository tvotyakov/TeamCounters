namespace TeamCounters.Application.Counters.Queries.GetCounterById;

public class CounterDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int TotalCount { get; set; }
}
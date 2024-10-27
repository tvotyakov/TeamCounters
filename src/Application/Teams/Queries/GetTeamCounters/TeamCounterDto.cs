namespace TeamCounters.Application.Teams.Queries.GetTeamCounters;

public class TeamCounterDto
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public int TotalCount { get; set; }
}
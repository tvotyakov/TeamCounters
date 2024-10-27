namespace TeamCounters.Application.Teams.Queries.GetTeamById;

public class TeamDto
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public int TotalCounts { get; set; }
}

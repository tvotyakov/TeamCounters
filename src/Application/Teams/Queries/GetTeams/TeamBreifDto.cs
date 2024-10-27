namespace TeamCounters.Application.Teams.Queries.GetTeams;

public class TeamBreifDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int TotalCount { get; set; }
}
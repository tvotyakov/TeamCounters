using TeamCounters.Domain.Abstract;

namespace TeamCounters.Domain.Teams;
public class Team : BaseEntity<Guid>
{
    public required string Name { get; init; }
}

namespace TeamCounters.Domain.Teams;
public interface ITeamsRepository
{
    ValueTask<Team> Add(Team team, CancellationToken cancellationToken = default);

    ValueTask Delete(Guid id, CancellationToken cancellationToken = default);

    ValueTask<Team?> GetById(Guid id, CancellationToken cancellationToken = default);

    ValueTask<IEnumerable<Team>> GetList();
}

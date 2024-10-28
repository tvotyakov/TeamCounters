using TeamCounters.Domain.Teams;

namespace TeamCounters.DataAccess.InMemory.Teams;
public class TeamsRepository : ITeamsRepository
{
    public ValueTask<Team> Add(Team team, CancellationToken cancellationToken = default)
    {
        team.Id = Guid.NewGuid();

        if (!Db.Teams.TryAdd(team.Id, team))
        {
            throw new ApplicationException("Team has not been added");
        }

        return ValueTask.FromResult(team);
    }

    public ValueTask Delete(Guid id, CancellationToken cancellationToken = default)
    {
        if (!Db.Teams.TryRemove(id, out var team))
        {
            throw new ApplicationException("Team has not been found");
        }

        foreach (var counter in team.Counters)
        {
            counter.Team = null;
        }

        return ValueTask.CompletedTask;
    }

    public ValueTask<Team?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        Db.Teams.TryGetValue(id, out var team);

        return ValueTask.FromResult(team);
    }

    public ValueTask<IEnumerable<Team>> GetList()
    {
        return ValueTask.FromResult(Db.Teams.Values.AsEnumerable());
    }

    public ValueTask<bool> IsExistByName(string name)
    {
        var isExist = Db.Teams
            .Values
            .Any(x => x.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));

        return ValueTask.FromResult(isExist);
    }
}

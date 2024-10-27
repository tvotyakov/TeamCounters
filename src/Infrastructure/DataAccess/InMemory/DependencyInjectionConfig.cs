using Microsoft.Extensions.DependencyInjection;

using TeamCounters.DataAccess.InMemory.Counters;
using TeamCounters.DataAccess.InMemory.Teams;
using TeamCounters.Domain.Counters;
using TeamCounters.Domain.Teams;

namespace TeamCounters.DataAccess.InMemory;
public static class DependencyInjectionConfig
{
    public static IServiceCollection AddInMemoryDataAccess(this IServiceCollection services)
    {
        services.AddTransient<ICountersRepository, CountersRepository>();
        services.AddTransient<ITeamsRepository, TeamsRepository>();

        return services;
    }
}

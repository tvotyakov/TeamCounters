using Microsoft.Extensions.DependencyInjection;

using TeamCounters.DataAccess.InMemory.Counters;
using TeamCounters.Domain.Counters;

namespace TeamCounters.DataAccess.InMemory;
public static class DependencyInjectionConfig
{
    public static IServiceCollection AddInMemoryDataAccess(this IServiceCollection services)
    {
        services.AddSingleton<ICountersRepository, CountersRepository>();
        return services;
    }
}

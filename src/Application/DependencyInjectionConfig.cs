using System.Reflection;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

namespace TeamCounters.Application;
public static class DependencyInjectionConfig
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddMediatR(conf =>
        {
            conf.RegisterServicesFromAssemblyContaining(typeof(DependencyInjectionConfig));
        });

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}

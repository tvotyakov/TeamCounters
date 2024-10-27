using System.Reflection;

using FluentValidation;

using MediatR.Pipeline;

using Microsoft.Extensions.DependencyInjection;

using TeamCounters.Application.Common.Behaviors;

namespace TeamCounters.Application;

public static class DependencyInjectionConfig
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddMediatR(conf =>
        {
            conf.RegisterServicesFromAssemblyContaining(typeof(DependencyInjectionConfig));
            conf.AddRequestPreProcessor(typeof(IRequestPreProcessor<>), typeof(LoggingPreProcessing<>));
            conf.AddRequestPostProcessor(typeof(IRequestPostProcessor<,>), typeof(LoggingPostProcessing<,>));
        });

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}

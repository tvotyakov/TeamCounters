using MediatR.Pipeline;

using Microsoft.Extensions.Logging;

namespace TeamCounters.Application.Common.Behaviors;
public class LoggingPreProcessing<TRequest>(ILogger<TRequest> logger) : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger<TRequest> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("TeamCounters request {Name} started: {@Request}", typeof(TRequest).Name, request);
        return Task.CompletedTask;
    }
}

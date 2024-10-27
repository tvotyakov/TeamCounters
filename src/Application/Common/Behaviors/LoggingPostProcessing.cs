using MediatR.Pipeline;

using Microsoft.Extensions.Logging;

namespace TeamCounters.Application.Common.Behaviors;
public class LoggingPostProcessing<TRequest, TResponse>(ILogger<TRequest> logger) :
    IRequestPostProcessor<TRequest, TResponse> where TRequest : notnull
{
    private readonly ILogger<TRequest> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
    {
        _logger.LogInformation("TeamCounters request {Name} finished: {@Request}", typeof(TRequest).Name, request);
        return Task.CompletedTask;
    }
}

using Ardalis.GuardClauses;

using FluentValidation;

using MediatR;

namespace TeamCounters.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) :
    IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators = validators ?? [];

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(next, nameof(next));

        if (_validators.Any())
        {
            var validationContext = new ValidationContext<TRequest>(request);

            var validationResults = await Task
                .WhenAll(_validators.Select(v => v.ValidateAsync(validationContext, cancellationToken)));

            var errors = validationResults
                .Where(x => !x.IsValid)
                .SelectMany(x => x.Errors)
                .ToList();

            if (errors.Count > 0)
            {
                throw new ValidationException(errors);
            }
        }

        return await next();
    }
}
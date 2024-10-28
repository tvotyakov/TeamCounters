using FluentValidation;

using MediatR;

namespace TeamCounters.Application.Counters.Commands.IncrementCounter;

public sealed record IncrementCounterCommand(Guid CounterId, int Value) : IRequest<int>;

public sealed class IncrementCounterCommandValidator : AbstractValidator<IncrementCounterCommand>
{
    public IncrementCounterCommandValidator()
    {
        RuleFor(x => x.CounterId)
            .NotEmpty();

        RuleFor(x => x.Value)
            .GreaterThan(0);
    }
}

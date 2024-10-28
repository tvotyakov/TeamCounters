
using FluentValidation;

using MediatR;

using TeamCounters.Domain.Counters;

namespace TeamCounters.Application.Counters.Commands.CreateCounter;

public sealed record CreateCounterCommand(string Name) : IRequest<Guid>;

public sealed class CreateCounterCommandValidator : AbstractValidator<CreateCounterCommand>
{
    private readonly ICountersRepository _counterRepo;

    public CreateCounterCommandValidator(ICountersRepository counterRepo)
    {
        _counterRepo = counterRepo;

        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(Counter.NameMinLength)
            .MaximumLength(Counter.NameMaxLength);

        RuleFor(x => x.Name)
            .MustAsync(BeAUniqueName)
            .WithMessage("Counter with this name already exists")
            .When(x => !string.IsNullOrWhiteSpace(x.Name));
    }

    private async Task<bool> BeAUniqueName(string name, CancellationToken token) =>
        !await _counterRepo.IsExistByName(name);
}
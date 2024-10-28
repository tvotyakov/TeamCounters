
using FluentValidation;

using MediatR;

using TeamCounters.Domain.Teams;

namespace TeamCounters.Application.Teams.Commands.CreateTeam;
public sealed record CreateTeamCommand(string Name) : IRequest<Guid>;

public sealed class CreateTeamCommandValidator : AbstractValidator<CreateTeamCommand>
{
    private readonly ITeamsRepository _teamsRepo;

    public CreateTeamCommandValidator(ITeamsRepository teamsRepo)
    {
        _teamsRepo = teamsRepo;

        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(Team.NameMinLength)
            .MaximumLength(Team.NameMaxLength);

        RuleFor(x => x.Name)
            .MustAsync(BeAUniqueName)
            .WithMessage("Team with this name already exists");
    }

    private async Task<bool> BeAUniqueName(string name, CancellationToken token) =>
        !await _teamsRepo.IsExistByName(name);
}
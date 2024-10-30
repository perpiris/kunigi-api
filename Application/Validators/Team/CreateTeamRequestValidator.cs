using Application.Contracts.Team;
using Application.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Validators.Team;

public class CreateTeamRequestValidator : AbstractValidator<CreateTeamRequest>
{
    private readonly IDataContext _context;

    public CreateTeamRequestValidator(IDataContext context)
    {
        _context = context;

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Tο πεδίο απαιτείται")
            .MaximumLength(100)
            .WithMessage("Το πεδίο δεν μπορεί να είναι πάνω απο 100 χαρακτήρες")
            .MustAsync(BeUniqueName)
            .WithMessage("Υπάρχει ήδη ομάδα με αυτό το όνομα");
    }

    private async Task<bool> BeUniqueName(string name, CancellationToken token)
    {
        return !await _context.Teams.AnyAsync(t => t.Name == name.Trim(), token);
    }
}
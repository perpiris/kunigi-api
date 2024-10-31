using Application.Contracts.Team;
using Application.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Validators.Team;

public class UpdateTeamRequestValidator : AbstractValidator<UpdateTeamRequest>
{
    private readonly IDataContext _context;

    public UpdateTeamRequestValidator(IDataContext context)
    {
        _context = context;

        RuleFor(x => x.TeamId)
            .NotEmpty()
            .WithMessage("Team Id is required")
            .MustAsync(ExistInDatabase)
            .WithMessage("Η ομάδα δεν βρέθηκε");

        RuleFor(x => x.CreatedYear)
            .InclusiveBetween((short)1990, (short)DateTime.UtcNow.Year)
            .WithMessage($"Created year must be between 1990 and {DateTime.UtcNow.Year}")
            .When(x => x.CreatedYear.HasValue);

        RuleFor(x => x.Website)
            .MaximumLength(150)
            .WithMessage("Το πεδίο δεν μπορεί να ξεπεράσει τους 150 χαρακτήρες")
            .When(x => !string.IsNullOrEmpty(x.Website));

        RuleFor(x => x.Facebook)
            .MaximumLength(150)
            .WithMessage("Το πεδίο δεν μπορεί να ξεπεράσει τους 150 χαρακτήρες")
            .When(x => !string.IsNullOrEmpty(x.Facebook));

        RuleFor(x => x.Youtube)
            .MaximumLength(150)
            .WithMessage("Το πεδίο δεν μπορεί να ξεπεράσει τους 150 χαρακτήρες")
            .When(x => !string.IsNullOrEmpty(x.Youtube));

        RuleFor(x => x.Instagram)
            .MaximumLength(150)
            .WithMessage("Το πεδίο δεν μπορεί να ξεπεράσει τους 150 χαρακτήρες")
            .When(x => !string.IsNullOrEmpty(x.Instagram));
    }

    private async Task<bool> ExistInDatabase(Guid teamId, CancellationToken token)
    {
        return await _context.Teams.AnyAsync(t => t.TeamId == teamId, token);
    }
}
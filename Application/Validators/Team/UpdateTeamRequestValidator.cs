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
            .WithMessage("Team not found");

        RuleFor(x => x.Description)
            .MaximumLength(500)
            .WithMessage("Description cannot exceed 500 characters")
            .When(x => x.Description != null);

        RuleFor(x => x.CreatedYear)
            .InclusiveBetween((short)1800, (short)DateTime.UtcNow.Year)
            .WithMessage($"Created year must be between 1800 and {DateTime.UtcNow.Year}")
            .When(x => x.CreatedYear.HasValue);

        RuleFor(x => x.Website)
            .MaximumLength(150)
            .WithMessage("Website URL cannot exceed 150 characters")
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
            .WithMessage("Website must be a valid URL")
            .When(x => !string.IsNullOrEmpty(x.Website));

        RuleFor(x => x.Facebook)
            .MaximumLength(150)
            .WithMessage("Facebook URL cannot exceed 150 characters")
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
            .WithMessage("Facebook must be a valid URL")
            .When(x => !string.IsNullOrEmpty(x.Facebook));

        RuleFor(x => x.Youtube)
            .MaximumLength(150)
            .WithMessage("Youtube URL cannot exceed 150 characters")
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
            .WithMessage("Youtube must be a valid URL")
            .When(x => !string.IsNullOrEmpty(x.Youtube));

        RuleFor(x => x.Instagram)
            .MaximumLength(150)
            .WithMessage("Instagram URL cannot exceed 150 characters")
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
            .WithMessage("Instagram must be a valid URL")
            .When(x => !string.IsNullOrEmpty(x.Instagram));
    }

    private async Task<bool> ExistInDatabase(Guid teamId, CancellationToken token)
    {
        return await _context.Teams.AnyAsync(t => t.TeamId == teamId, token);
    }
}
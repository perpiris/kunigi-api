using Application.Contracts.Common;
using Application.Contracts.Team;
using Application.Interfaces;
using Application.Mappings;
using Application.Utilities;
using Domain.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class TeamService : ITeamService
{
    private readonly IDataContext _context;
    private readonly IFileManagerService _fileManagerService;
    private readonly IValidator<CreateTeamRequest> _createTeamValidator;
    private readonly IValidator<UpdateTeamRequest> _updateTeamValidator;

    public TeamService(
        IDataContext context,
        IFileManagerService fileManagerService,
        IValidator<CreateTeamRequest> createTeamValidator,
        IValidator<UpdateTeamRequest> updateTeamValidator)
    {
        _context = context;
        _fileManagerService = fileManagerService;
        _createTeamValidator = createTeamValidator;
        _updateTeamValidator = updateTeamValidator;
    }

    public async Task<Result> CreateTeam(CreateTeamRequest request)
    {
        var validationResult = await _createTeamValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return Result.Failure(validationResult.Errors.Select(e => e.ErrorMessage).ToArray());
        }

        var slug = SlugGenerator.GenerateSlug(request.Name.Trim());
        var newTeam = new Team
        {
            Name = request.Name.Trim(),
            Slug = slug,
            IsActive = request.IsActive
        };
        
        _fileManagerService.CreateFolder($"teams/{slug}");

        _context.Teams.Add(newTeam);
        await _context.SaveChangesAsync(CancellationToken.None);

        return Result.Success();
    }

    public async Task<Result<TeamResponse>> GetTeamById(Guid id)
    {
        var result = await _context.Teams
            .Include(x => x.WonGames)
            .Include(x => x.HostedGames)
            .FirstOrDefaultAsync(x => x.TeamId == id);
        
        return result == null
            ? Result<TeamResponse>.Failure("Η ομάδα δεν βρέθηκε")
            : Result<TeamResponse>.Success(result.ToTeamResponse(true));
    }

    public async Task<PagedList<TeamResponse>> GetPagedTeams(int pageNumber, int pageSize)
    {
        var query = _context.Teams.AsQueryable();
        var totalItems = await query.CountAsync();

        var teams = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(x => x.ToTeamResponse(false))
            .ToListAsync();

        return new PagedList<TeamResponse>
        {
            Items = teams,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = totalItems
        };
    }
    
    public async Task<List<SelectListItem>> GetTeamSelectList()
    {
        return await _context.Teams.Select(x => x.ToSelectListItem()).ToListAsync();
    }

    public async Task<Result> UpdateTeam(UpdateTeamRequest request)
    {
        var validationResult = await _updateTeamValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return Result.Failure(validationResult.Errors.Select(e => e.ErrorMessage).ToArray());
        }

        var team = (await _context.Teams.FindAsync(request.TeamId))!;

        team.Description = request.Description?.Trim();
        team.CreatedYear = request.CreatedYear;
        team.IsActive = request.IsActive;
        team.Website = request.Website?.Trim();
        team.Facebook = request.Facebook?.Trim();
        team.Youtube = request.Youtube?.Trim();
        team.Instagram = request.Instagram?.Trim();

        // if (profileImageStream is not null)
        // {
        //     var profileImagePath = await _fileManagerService.SaveFile(profileImageStream, $"teams/{team.Slug.Trim()}");
        //     team.ProfileImagePath = profileImagePath;
        // }

        _context.Teams.Update(team);
        await _context.SaveChangesAsync(CancellationToken.None);
        return Result.Success();
    }
}
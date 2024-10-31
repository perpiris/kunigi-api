using Application.Contracts.Common;
using Application.Contracts.Game;
using Application.Interfaces;
using Application.Mappings;
using Application.Utilities;
using Domain.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class GameService : IGameService
{
    private readonly IDataContext _context;
    private readonly IValidator<CreateParentGameRequest> _createParentGameValidator;
    private readonly IValidator<UpdateParentGameRequest> _updateParentGameValidator;

    public GameService(IDataContext context, 
        IValidator<CreateParentGameRequest> createParentGameValidator, 
        IValidator<UpdateParentGameRequest> updateParentGameValidator)
    {
        _context = context;
        _createParentGameValidator = createParentGameValidator;
        _updateParentGameValidator = updateParentGameValidator;
    }

    public async Task<Result> CreateParentGame(CreateParentGameRequest request)
    {
        var validationResult = await _createParentGameValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return Result.Failure(validationResult.Errors.Select(e => e.ErrorMessage).ToArray());
        }
        
        var newParentGame = new ParentGame
        {
            Year = request.Year,
            Order = request.Order,
            Slug = SlugGenerator.GenerateSlug(request.Year.ToString()),
            MainTitle = $"{request.Order}ο Κυνήγι Θησαυρού",
            WinnerId = request.WinnerId,
            HostId = request.HostId,
        };

        _context.ParentGames.Add(newParentGame);
        await _context.SaveChangesAsync(CancellationToken.None);

        return Result.Success();
    }

    public async Task<Result<ParentGameResponse>> GetParentGameById(Guid id)
    {
        var result = await _context.ParentGames
            .Include(x => x.Host)
            .Include(x => x.Winner)
            .Where(x => x.ParentGameId == id)
            .FirstOrDefaultAsync();
        
        return result == null
            ? Result<ParentGameResponse>.Failure("Το παιχνίδι δεν βρέθηκε")
            : Result<ParentGameResponse>.Success(result.ToParentGameResponse(true));
    }

    public async Task<PagedList<ParentGameResponse>> GetPagedParentGames(int pageNumber, int pageSize)
    {
        var query = _context.ParentGames.AsQueryable();
        var totalItems = await query.CountAsync();

        var parentGames = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(x => x.ToParentGameResponse(false))
            .ToListAsync();

        return new PagedList<ParentGameResponse>
        {
            Items = parentGames,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = totalItems
        };
    }

    public async Task<Result> UpdateParentGame(UpdateParentGameRequest request)
    {
        var validationResult = await _updateParentGameValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return Result.Failure(validationResult.Errors.Select(e => e.ErrorMessage).ToArray());
        }

        var parentGame = (await _context.ParentGames.FindAsync(request.ParentGameId))!;
        

        await _context.SaveChangesAsync(CancellationToken.None);
        return Result.Success();
    }
}
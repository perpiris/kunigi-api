using Application.Contracts.Common;
using Application.Contracts.Game;

namespace Application.Interfaces;

public interface IGameService
{
    Task<Result> CreateParentGame(CreateParentGameRequest request);

    Task<Result<ParentGameResponse>> GetParentGameById(Guid id);

    Task<PagedList<ParentGameResponse>> GetPagedParentGames(int pageNumber, int pageSize);
    
    Task<Result> UpdateParentGame(UpdateParentGameRequest request);
}
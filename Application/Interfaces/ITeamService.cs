using Application.Contracts.Common;
using Application.Contracts.Team;

namespace Application.Interfaces;

public interface ITeamService
{
    Task<Result> CreateTeam(CreateTeamRequest request);

    Task<Result<TeamResponse>> GetTeamById(Guid id);

    Task<PagedList<TeamResponse>> GetPagedTeams(int pageNumber, int pageSize);
    
    Task<List<SelectListItem>> GetTeamSelectList();
    
    Task<Result> UpdateTeam(UpdateTeamRequest request, Stream? profileImageStream);
}
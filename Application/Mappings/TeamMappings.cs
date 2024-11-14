using Application.Contracts.Common;
using Application.Contracts.Team;
using Domain.Entities;

namespace Application.Mappings;

public static class TeamMappings
{
    public static TeamResponse ToTeamResponse(this Team team, bool includeAll)
    {
        var response = new TeamResponse
        {
            TeamId = team.TeamId,
            Name = team.Name,
            Slug = team.Slug,
            Description = team.Description,
            CreatedYear = team.CreatedYear,
            IsActive = team.IsActive,
            Website = team.Website,
            Facebook = team.Facebook,
            Youtube = team.Youtube,
            Instagram = team.Instagram,
            ProfileImagePath = team.ProfileImagePath,
            WonGames = [],
            HostedGames = []
        };

        if (includeAll)
        {
            response.WonGames = team.WonGames.Select(x => x.ToParentGameResponse(false)).ToList();
            response.HostedGames = team.HostedGames.Select(x => x.ToParentGameResponse(false)).ToList();
        }

        return response;
    }

    public static SelectListItem ToSelectListItem(this Team team)
    {
        return new SelectListItem
        {
            Id = team.TeamId.ToString(),
            Label = team.Name,
        };
    }
}
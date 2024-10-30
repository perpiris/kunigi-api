using Application.Contracts.Team;
using Domain.Entities;

namespace Application.Mappings;

public static class TeamMappings
{
    public static TeamResponse ToTeamResponse(this Team team)
    {
        return new TeamResponse
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
            ProfileImagePath = team.ProfileImagePath
        };
    }
}
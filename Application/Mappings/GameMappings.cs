using Application.Contracts.Game;
using Domain.Entities;

namespace Application.Mappings;

public static class GameMappings
{
    public static ParentGameResponse ToParentGameResponse(this ParentGame parentGame, bool includeAll)
    {
        var response = new ParentGameResponse
        {
            ParentGameId = parentGame.ParentGameId,
            Year = parentGame.Year,
            Order = parentGame.Order,
            MainTitle = parentGame.MainTitle,
            SubTitle = parentGame.SubTitle,
            Description = parentGame.Description,
            Slug = parentGame.Slug,
            ProfileImageUrl = parentGame.ProfileImagePath
        };

        if (includeAll)
        {
            response.Winner = parentGame.Winner.Name;
            response.WinnerId = parentGame.Winner.TeamId;
            response.Host = parentGame.Host.Name;
            response.HostId = parentGame.Host.TeamId;
        }

        return response;
    }
}
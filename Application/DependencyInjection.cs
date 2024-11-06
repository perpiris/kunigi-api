using Application.Interfaces;
using Application.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        
        services.AddScoped<ITeamService, TeamService>();
        services.AddScoped<IGameService, GameService>();
        services.AddScoped<IFileManagerService, FileManagerService>();
        services.AddScoped<IdentityService, IdentityService>();
        
        return services;
    }
}
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;

public interface IDataContext
{
    DbSet<Team> Teams { get; }
    
    DbSet<ParentGame> ParentGames { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
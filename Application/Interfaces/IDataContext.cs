using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;

public interface IDataContext
{
    DbSet<Team> Teams { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
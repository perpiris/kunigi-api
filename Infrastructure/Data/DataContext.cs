using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DataContext : DbContext, IDataContext
{
    public DbSet<Team> Teams { get; set; }
    
    public DbSet<ParentGame> ParentGames { get; set; }
    
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<ParentGame>()
            .HasOne(x => x.Host)
            .WithMany(x => x.HostedGames)
            .HasForeignKey(x => x.HostId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ParentGame>()
            .HasOne(x => x.Winner)
            .WithMany(x => x.WonGames)
            .HasForeignKey(x => x.WinnerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
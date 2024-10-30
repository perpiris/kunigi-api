using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DataContext : DbContext, IDataContext
{
    public DbSet<Team> Teams { get; set; }
    
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class ParentGame
{
    [Key]
    public Guid ParentGameId { get; set; }

    public required short Year { get; set; }

    public required short Order { get; set; }
    
    [MaxLength(150)]
    public required string Slug { get; set; }
    
    [MaxLength(150)]
    public required string MainTitle { get; set; }
    
    [MaxLength(150)]
    public string? SubTitle { get; set; }
    
    public string? Description { get; set; }
    
    [MaxLength(150)]
    public string? ProfileImagePath { get; set; }
    
    public Guid HostId { get; set; }
    
    public Guid WinnerId { get; set; }
    
    [ForeignKey("HostId")]
    public Team Host { get; set; } = null!;

    [ForeignKey("WinnerId")]
    public Team Winner { get; set; } = null!;
}
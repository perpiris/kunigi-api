using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Team
{
    [Key]
    public Guid TeamId { get; set; }
    
    [MaxLength(100)]
    public required string Name { get; set; }
    
    [MaxLength(100)]
    public required string Slug { get; set; }
    
    public string? Description { get; set; }
    
    public short? CreatedYear { get; set; }
    
    public bool IsActive { get; set; }
    
    [MaxLength(150)]
    public string? Website { get; set; }
    
    [MaxLength(150)]
    public string? Facebook { get; set; }
    
    [MaxLength(150)]
    public string? Youtube { get; set; }
    
    [MaxLength(150)]
    public string? Instagram { get; set; }
    
    [MaxLength(150)]
    public string? ProfileImagePath { get; set; }
}
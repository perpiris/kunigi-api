namespace Application.Contracts.Game;

public class ParentGameResponse
{
    public Guid ParentGameId { get; set; }
    
    public short Year { get; set; }

    public short Order { get; set; }
    
    public required string MainTitle { get; set; }
    
    public string? SubTitle { get; set; }

    public string? Description { get; set; }
    
    public required string Slug { get; set; }
    
    public string? ProfileImageUrl { get; set; }
    
    public Guid WinnerId { get; set; }

    public string? Winner { get; set; }
    
    public Guid HostId { get; set; }
    
    public string? Host { get; set; }
}
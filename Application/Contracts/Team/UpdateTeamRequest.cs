namespace Application.Contracts.Team;

public class UpdateTeamRequest
{
    public Guid TeamId { get; set; }
    
    public string? Description { get; set; }
    
    public short? CreatedYear { get; set; }
    
    public bool IsActive { get; set; }
    
    public string? Website { get; set; }
    
    public string? Facebook { get; set; }
    
    public string? Youtube { get; set; }
    
    public string? Instagram { get; set; }
}
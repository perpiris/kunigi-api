namespace Application.Contracts.Team;

public class CreateTeamRequest
{
    public required string Name { get; set; }
    
    public bool IsActive { get; set; }
}
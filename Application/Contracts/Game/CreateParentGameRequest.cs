namespace Application.Contracts.Game;

public class CreateParentGameRequest
{
    public short Year { get; set; }

    public short Order { get; set; }
    
    public string? SubTitle { get; set; }
    
    public Guid HostId { get; set; }
    
    public Guid WinnerId { get; set; }
}
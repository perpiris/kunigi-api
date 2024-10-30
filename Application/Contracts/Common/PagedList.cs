namespace Application.Contracts.Common;

public class PagedList<T>
{
    public required List<T> Items { get; set; }
    
    public required int PageNumber { get; set; }
    
    public required int PageSize { get; set; }
    
    public required int TotalItems { get; set; }
}
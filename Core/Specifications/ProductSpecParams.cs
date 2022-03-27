namespace Core.Specifications;

public class ProductSpecParams
{
    // Sorting
    public string Sort { get; init; } = string.Empty;
    public bool Desc { get; init; }
    
    // Filtering
    public int? BrandId { get; init; }
    public int? TypeId { get; init; }
    
    // Pagination
    public int PageIndex { get; init; }
    
    private const int MaxPageSize = 50;
    private int _pageSize;
    public int PageSize
    {
        get => _pageSize;
        init => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }

    private string _search = string.Empty;
    public string Search
    {
        get => _search; 
        init => _search = (string.IsNullOrWhiteSpace(value)) 
            ? string.Empty 
            : value.Trim().ToLower();
    }
}
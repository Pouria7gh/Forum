namespace Application.Core;

public class PagingParams
{
    private int _maxPageSize = 10;

    public int CurrentPage { get; set; } = 1;

    private int _pageSize = 10;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > _maxPageSize) ? _maxPageSize : value;
    }
}

using Microsoft.EntityFrameworkCore;

namespace Application.Core;

public class PagedList<T> : List<T>
{
    public PagedList(IEnumerable<T> items, int currentPage, int totalCount, int pageSize)
    {
        CurrentPage = currentPage;
        TotalCount = totalCount;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling((decimal)totalCount / (decimal)pageSize);
        AddRange(items);
    }
    public int TotalCount { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }

    public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int currentPage, int pageSize)
    {
        if (currentPage == 0) currentPage = 1;
        if (pageSize == 0) pageSize = 10;

        int totalCount = await source.CountAsync();

        var pagedSource = source.Skip((currentPage - 1) * pageSize).Take(pageSize);

        var items = await pagedSource.ToListAsync();

        return new PagedList<T>(items, currentPage, totalCount, pageSize);
    }
}
using Application.Core;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Forum.Web.Framework.Pagination;

public static class HttpContextPaginationExtentions
{
    public static HttpContext AddPaginationHeader<T>(this HttpContext httpContext, PagedList<T> pagedList)
    {
        var paging = new
        {
            CurrentPage = pagedList.CurrentPage,
            PageSize = pagedList.PageSize,
            TotalCount = pagedList.TotalCount,
            TotalPages = pagedList.TotalPages
        };

        httpContext.Response.Headers.TryAdd("Pagination", JsonSerializer.Serialize(paging));

        return httpContext;
    }

    public static bool TryGetPaginationFromHeaders(this HttpContext httpContext, out Pagination? pagination)
    {
        var headers = httpContext.Response.Headers;
        if (headers.TryGetValue("Pagination", out var paginationJson))
        {
            pagination = JsonSerializer.Deserialize<Pagination>(paginationJson.ToString());
            return true;
        }
        pagination = null;
        return false;
    }
}
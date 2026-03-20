using Microsoft.AspNetCore.Mvc;
using Application.Forum;
using Application.DTOs.ForumRoom;
using MediatR;
using Application.Core;
using Forum.Web.Framework.Pagination;

namespace Forum.Web.Components;

public class ListPostRepliesViewComponent : ViewComponent
{
    private readonly IMediator _mediator;
    public ListPostRepliesViewComponent(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<IViewComponentResult> InvokeAsync(Guid parentPostId)
    {
        PagingParams pagingParams = HttpContext.GetPagingParamsFromQuery();
        ListPostReplies.Query query = new() 
        { 
            ParentPostId = parentPostId,
            PagingParams = pagingParams
        };
        var result = await _mediator.Send(query);

        HttpContext.AddPaginationHeader(result.Value!);
        return View(result.Value);
    }
}

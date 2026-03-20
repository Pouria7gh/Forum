using Application.DTOs.ForumRoom;
using Application.Forum;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Forum.Web.Framework.Pagination;

namespace Forum.Web.Components;

public class ListRoomPosts : ViewComponent
{
    private readonly IMediator _mediator;
    public ListRoomPosts(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<IViewComponentResult> InvokeAsync(Guid forumRoomId)
    {
        var pagingParams = HttpContext.GetPagingParamsFromQuery();

        ListForumRoomPosts.Query query = new()
        {
            ForumRoomId = forumRoomId,
            PagingParams = pagingParams
        };

        var result = await _mediator.Send(query);
    
        if (result.Succeed)
        {
            HttpContext.AddPaginationHeader(result.Value!);
            return View(result.Value);
        }
        else
        {
            return View(new List<ForumPostDto>());
        }
    }
}
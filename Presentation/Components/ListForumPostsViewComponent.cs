using Application.DTOs.ForumRoom;
using Application.Forum;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.Components;

public class ListForumPostsViewComponent : ViewComponent
{
    private readonly IMediator _mediator;

    public ListForumPostsViewComponent(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<IViewComponentResult> InvokeAsync(Guid forumRoomId)
    {
        ListForumRoomPosts.Query query = new()
        {
            ForumRoomId = forumRoomId
        };

        var result = await _mediator.Send(query);
    
        if (result.Succeed)
        {
            return View(result.Value);
        }
        else
        {
            return View(new List<ForumPostDto>());
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Application.Forum;
using Application.DTOs.ForumRoom;
using MediatR;

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
        ListPostReplies.Query query = new() { ParentPostId = parentPostId };
        var result = await _mediator.Send(query);

        if (!result.Succeed)
            return View(new List<ForumPostDto>());

        return View(result.Value);
    }
}

using Application.Forum;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers;

namespace Forum.Web.Controllers;

public class ForumRoomController : BaseController
{
    [HttpGet("ForumRoom/{id}")]
    public async Task<IActionResult> IndexAsync(Guid id)
    {
        var query = new GetForumRoom.Query() { ForumRoomId = id };
        var result = await Mediator.Send(query);

        return View(result.Value);
    }
}

using Application.Forum;
using Forum.Web.Models.ForumPost;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers;

namespace Forum.Web.Controllers;

public class ForumPostController : BaseController
{
    [HttpGet("ForumPosts/{forumRoomId}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> IndexAsync(Guid forumRoomId)
    {
        var query = new ListForumRoomPosts.Query() { ForumRoomId = forumRoomId };

        var result = await Mediator.Send(query);

        if (result.Succeed)
        {
            return Ok(result.Value);
        }
        else
        {
            return BadRequest(result.ErrorMessage);
        }
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddForumPost([FromBody] AddForumPostViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var command = new AddForumPost.Command()
        {
            PostContent = model.PostContent,
            Description = model.Description,
            ForumRoomId = model.ForumRoomId,
            UserId = model.UserId
        };

        var result = await Mediator.Send(command);

        if (result.Succeed)
        {
            return Ok();
        }
        else
        {
            return BadRequest(result.ErrorMessage);
        }
    }
}

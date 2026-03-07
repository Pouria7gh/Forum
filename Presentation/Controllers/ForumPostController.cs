using Application.Forum;
using Forum.Web.Models.ForumPost;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers;
using System.Security.Claims;

namespace Forum.Web.Controllers;

public class ForumPostController : BaseController
{
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddForumPost(AddForumPostViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var command = new AddForumPost.Command()
        {
            PostContent = model.PostContent.Trim(),
            Description = model.Description?.Trim(),
            ForumRoomId = model.ForumRoomId,
            UserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value),
            ParentPostId = model.ParentPostId
        };

        var result = await Mediator.Send(command);

        if (result.Succeed)
        {
            return Redirect($"/ForumRoom/{model.ForumRoomId}");
        }
        else
        {
            return BadRequest(result.ErrorMessage);
        }
    }
}

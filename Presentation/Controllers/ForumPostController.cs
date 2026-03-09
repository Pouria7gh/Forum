using Application.DTOs.ForumRoom;
using Application.Forum;
using Forum.Web.Framework.Mvc.Filters;
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
    [ParameterBasedOnFormName("redirect-to-parent-post", "redirectToParentPost")]
    public async Task<IActionResult> AddForumPost(AddForumPostViewModel model, bool redirectToParentPost)
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

        if (!result.Succeed)
        {
            return BadRequest(result.ErrorMessage);
        }

        if (redirectToParentPost)
        {
            return Redirect($"/ForumPost/{model.ParentPostId}");
        }
        else
        {
            return Redirect($"/ForumRoom/{model.ForumRoomId}");
        }
    }

    [HttpGet("/ForumPost/{postId}")]
    public async Task<IActionResult> IndexAsync(Guid postId)
    {
        ForumPostDetails.Query query = new() { PostId = postId };
        var result = await Mediator.Send(query);
        
        if (result.Succeed)
        {
            return View(result.Value);
        }
        else
        {
            return View(new ForumPostDto());
        }
    }
}

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
    public async Task<IActionResult> AddForumPost(AddForumPostViewModel model, string returnUrl)
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
            SetSuccess("Post Added Successfully");
        }
        else
        {
            SetError(result.ErrorMessage!);
        }
        return Redirect(returnUrl);
    }

    [HttpGet("/ForumPost/{postId}")]
    public async Task<IActionResult> IndexAsync(Guid postId)
    {
        await Mediator.Send(new AddPostView.Command() { ForumPostId = postId });

        ForumPostDetails.Query query = new() { PostId = postId };
        var result = await Mediator.Send(query);
        
        if (result.Succeed)
        {
            return View(result.Value);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddInteraction([FromBody] AddPostInteraction.Command command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Problem Adding Interaction.");
        }

        var result = await Mediator.Send(command);

        if (result.Succeed)
        {
            return Ok();
        }

        return BadRequest(result.ErrorMessage);
    }
}

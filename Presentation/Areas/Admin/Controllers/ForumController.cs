using Application.Forum;
using Forum.Web.Framework.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Presentation.Areas.Admin.Models.Forum;

namespace Presentation.Areas.Admin.Controllers;

public class ForumController : BaseAdminController
{
    [HttpGet]
    public IActionResult CreateForumRoom()
    {
        return View();
    }

    [HttpPost]
    [ParameterBasedOnFormName("save-continue", "continueEditing")]
    public async Task<IActionResult> CreateForumRoomAsync(CreateForumRoomViewModel model, bool continueEditing)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var forumRoomId = Guid.NewGuid();

        var command = new CreateForumRoom.Command()
        {
            Id = forumRoomId,
            Description = model.Description,
            Rules = model.Rules,
            Title = model.Title,
            Subtitle = model.Subtitle,
            IsClosed = model.IsClosed,
            IsPinned = model.IsPinned
        };

        var result = await Mediator.Send(command);

        if (result.Succeed)
        {
            return View();
        }
        else
        {
            ViewData["Error"] = result.ErrorMessage;
            return View(model);
        }
    }
}

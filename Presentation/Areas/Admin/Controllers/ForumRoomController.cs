using Application.Forum;
using Forum.Web.Areas.Admin.Models.ForumRoom;
using Forum.Web.Framework.Mvc.Filters;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.Controllers;

public class ForumRoomController : BaseAdminController
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
            if (continueEditing)
            {
                return View(model);
            }
            else
            {
                return Redirect("/Admin/ForumRoom/ListForumRooms");
            }
        }
        else
        {
            ViewData["Error"] = result.ErrorMessage;
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> ListForumRooms()
    {
        ListForumRooms.Query query = new();

        var result = await Mediator.Send(query);

        if (result.Succeed)
        {
            var forumRooms = result.Value!.Select(x => new ForumRoomViewModel()
            {
                Id = x.Id,
                Description = x.Description,
                IsClosed = x.IsClosed,
                IsPinned = x.IsPinned,
                Rules = x.Rules,
                Subtitle = x.Subtitle,
                Title = x.Title
            }).ToList();

            return View(forumRooms);
        }
        else
        {
            return View(new ForumRoomViewModel());
        }
    }

    [HttpGet]
    public async Task<IActionResult> Manage(Guid id)
    {
        var query = new GetForumRoomManageModel.Query() { Id = id };
        var result = await Mediator.Send(query);

        if (!result.Succeed) return BadRequest();

        var model = new ManageForumRoomViewModel()
        {
            Id = result.Value!.Id,
            Title = result.Value.Title,
            IsClosed = result.Value.IsClosed
        };
        return View(model);
    }

    [HttpPost]
    [RequireAntiforgeryToken]
    public async Task<IActionResult> UpdateIsClosed([FromBody] Guid forumRoomId)
    {
        var query = new UpdateIsClosed.Query() { ForumRoomId = forumRoomId };
        var result = await Mediator.Send(query);

        if (!result.Succeed) return BadRequest(result.ErrorMessage);

        return Ok();
    }
}

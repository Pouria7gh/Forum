using Application.Forum;
using Forum.Web.Areas.Admin.Models.Forum;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using System.Diagnostics;

namespace Presentation.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

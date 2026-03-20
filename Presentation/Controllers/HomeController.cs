using Application.Core;
using Application.Forum;
using Forum.Web.Areas.Admin.Models.ForumRoom;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using System.Diagnostics;
using Forum.Web.Framework.Pagination;

namespace Presentation.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index(PagingParams pagingParams)
        {
            ListForumRooms.Query query = new() { pagingParams = pagingParams };

            var result = await Mediator.Send(query);

            if (result.Succeed)
            {
                HttpContext.AddPaginationHeader(result.Value!);
                return View(result.Value);
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

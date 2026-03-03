using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers;

namespace Presentation.Areas.Admin.Controllers
{
    public class DashboardController : BaseAdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

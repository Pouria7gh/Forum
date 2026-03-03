using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Constraints;
using Presentation.Controllers;

namespace Presentation.Areas.Admin.Controllers
{
    [Area(AreaNames.ADMIN)]
    [Authorize(Roles = RoleNames.ADMIN)]
    public class BaseAdminController : BaseController
    {

    }
}

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class BaseController : Controller
    {
        private IMediator? _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;

        protected void SetError(string message)
        {
            TempData["Error"] = message;
        }

        protected void SetSuccess(string message)
        {
            TempData["Success"] = message;
        }
    }
}

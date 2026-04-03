using Application.Account;
using Application.User;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.Account;

namespace Presentation.Controllers
{
    public class AccountController : BaseController
    {
        [HttpGet]
        public IActionResult SignUp([FromQuery] string? returnUrl)
        {
            if (User.Identity == null || User.Identity.IsAuthenticated)
            {
                return Redirect(returnUrl ?? "/");
            }

            ViewData["returnUrl"] = returnUrl ?? "/";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await Mediator.Send(new SignUp.Command()
            {
                Username = model.Username,
                Email = model.Email,
                DisplayName = model.DisplayName,
                Password = model.Password
            });

            if (!result.Succeed)
            {
                SetError(result.ErrorMessage!);
                return View(model);
            }
           
            SetSuccess("Signup Successful");
            return Redirect(returnUrl);
        }

        [HttpGet]
        public IActionResult Login([FromQuery] string? returnUrl)
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return Redirect(returnUrl ?? "/");
            }

            ViewData["returnUrl"] = returnUrl ?? "/";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            var result = await Mediator.Send(new Login.Command()
            {
                Email = model.Email,
                Password = model.Password
            });

            if (!result.Succeed)
            {
                SetError(result.ErrorMessage!);
                return View("Login", model);
            }

            SetSuccess("Login successful");
            return Redirect(returnUrl);
            
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string returnUrl)
        {
            var result = await Mediator.Send(new Logout.Command());

            if (result.Succeed)
            {
                SetSuccess("Logout Successfull.");
            }
            else
            {
                SetError("Logout Failed.");
            }

            return Redirect(returnUrl ?? "/");
        }
    }
}

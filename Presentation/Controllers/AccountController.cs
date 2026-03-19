using Application.Account;
using Application.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.Account;
using System.Security.Claims;

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

            await SignInWithCookie(result.Value!.UserId);
           
            SetSuccess("Signup Successful");
            return Redirect(returnUrl);
        }

        private async Task SignInWithCookie(Guid userId, List<string>? roles = null)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, userId.ToString())
            };

            if (roles != null)
            {
                foreach(string role in roles)
                {
                    claims.Add(new(ClaimTypes.Role, role));
                }
            }

            var identity = new ClaimsIdentity(claims, "Cookies");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("Cookies", principal);
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

            await SignInWithCookie(result.Value!.UserId, result.Value!.Roles);

            SetSuccess("Login successful");
            return Redirect(returnUrl);
            
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string returnUrl)
        {
            await HttpContext.SignOutAsync();

            SetSuccess("Logout Successfull");
            return Redirect(returnUrl ?? "/");
        }
    }
}

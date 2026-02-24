using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using Application.User;

namespace Presentation.Controllers
{
    public class AccountController : BaseController
    {
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserViewModel model)
        {
            var result = await Mediator.Send(new CreateAccount.Command()
            {
                Username = model.Username,
                Email = model.Email,
                DisplayName = model.DisplayName,
                Password = model.Password
            });

            if (result.Succeed)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}

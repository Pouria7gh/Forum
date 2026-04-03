using Application.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence.Providers;
using System.Security.Claims;

namespace Infrastructure.Security;

public class SignInServiceImp : SignInService
{
    private readonly DataContext _dataContext;
    private readonly HttpContext _httpContext;
    public SignInServiceImp(DataContext dataContext, IHttpContextAccessor httpContextAccessor)
    {
        _dataContext = dataContext;
        _httpContext = httpContextAccessor.HttpContext!;
    }
    public async Task SignInAsync(Guid userId)
    {
        var roles = await _dataContext.Roles
                    .Where(x => x.UserRoles.Any(x => x.UserId == userId))
                    .Select(x => x.Name)
                    .ToListAsync();

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, userId.ToString())
        };

        if (roles != null)
        {
            foreach (string role in roles)
            {
                claims.Add(new(ClaimTypes.Role, role));
            }
        }

        var identity = new ClaimsIdentity(claims, "Cookies");
        var principal = new ClaimsPrincipal(identity);

        await _httpContext.SignInAsync("Cookies", principal);
    }
}
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrastructure.Security;

public class UserAccessorImp : UserAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserAccessorImp(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public Guid? GetUserId()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null) return null;

        var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);

        if (userId == null) return null;

        return Guid.Parse(userId.Value);
    }
}
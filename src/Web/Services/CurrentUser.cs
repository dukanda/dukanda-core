using System.Security.Claims;
using DukandaCore.Application.Common.Interfaces;

namespace DukandaCore.Web.Services;

public class CurrentUser : IUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? Id =>
        !string.IsNullOrEmpty(_httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier))
            ? Guid.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!)
        : null;

    public bool IsInRole(string roleName)
    {
        return _httpContextAccessor.HttpContext?.User?.IsInRole(roleName) ?? false;
    }
}